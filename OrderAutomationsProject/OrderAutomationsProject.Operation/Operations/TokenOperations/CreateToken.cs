using AutoMapper;
using Azure.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OrderAutomationsProject.Base.Encryption;
using OrderAutomationsProject.Base.Response;
using OrderAutomationsProject.Base.Token;
using OrderAutomationsProject.Data.Context;
using OrderAutomationsProject.Data.Domain;
using OrderAutomationsProject.Data.UnitOfWorks;
using OrderAutomationsProject.Operation.Cqrs;
using OrderAutomationsProject.Schema;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace OrderAutomationsProject.Operation.Operations.TokenOperations;

public class CreateTokenCommandHandler : IRequestHandler<CreateTokenCommand, ApiResponse<TokenResponse>>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    private readonly JwtConfig jwtConfig;

    public CreateTokenCommandHandler(IUnitOfWork unitOfWork, IOptionsMonitor<JwtConfig> jwtConfig, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.jwtConfig = jwtConfig.CurrentValue;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<TokenResponse>> Handle(CreateTokenCommand request, CancellationToken cancellationToken)
    {
        // Is the information received from the user correct?
        Dealer dealer = unitOfWork.DealerRepository.FirstOrDefault(x => x.Email == request.Model.Email);
        if (dealer == null)
        {
            var res = new ApiResponse<TokenResponse>("Invalid dealer informations");
            res.StatusCode = 401;
            res.Success = false;
            return res;
        }

        var md5 = Md5.Create(request.Model.Password.ToLower());

        if (dealer.PasswordHash != md5)
        {
            dealer.PasswordRetryCount++;

            await unitOfWork.CompleteAsync(cancellationToken);

            var res = new ApiResponse<TokenResponse>("Invalid dealer informations");
            res.StatusCode = 401;
            res.Success = false;
            return res;
        }

        if (!dealer.IsActive)
        {
            var res = new ApiResponse<TokenResponse>("Invalid dealer informations");
            res.StatusCode = 401;
            res.Success = false;
            return res;
        }

        string token = Token(dealer);

        TokenResponse tokenResponse = new()
        {
            Token = token,
            ExpireDate = DateTime.Now.AddMinutes(jwtConfig.AccessTokenExpiration),
            DealerNumber = dealer.DealerNumber,
            Email = dealer.Email,
            RefreshToken = CreateRefreshToken(),
            FirstName = dealer.FirstName,
            LastName = dealer.LastName,
            Role = dealer.Role,
            DealerId = dealer.Id
        };

        dealer.RefreshToken = tokenResponse.RefreshToken;
        dealer.RefreshTokenExpireDate = tokenResponse.ExpireDate;

        unitOfWork.DealerRepository.Update(dealer);
        await unitOfWork.CompleteAsync(cancellationToken);

        return new ApiResponse<TokenResponse>(tokenResponse);
    }

    private string Token(Dealer dealer)
    {
        Claim[] claims = GetClaims(dealer);
        var secret = Encoding.ASCII.GetBytes(jwtConfig.Secret);
        var jwtToken = new JwtSecurityToken(
            jwtConfig.Issuer,
            jwtConfig.Audience,
            claims,
            expires: DateTime.Now.AddMinutes(jwtConfig.AccessTokenExpiration),
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256Signature)
            );

        string accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);

        return accessToken;
    }

    private Claim[] GetClaims(Dealer dealer)
    {
        var claims = new[]
        {
            new Claim("Id", dealer.Id.ToString()),
            new Claim("DealerNumber", dealer.DealerNumber.ToString()),
            new Claim("Role", dealer.Role),
            new Claim("Email", dealer.Email),
            new Claim(ClaimTypes.Role, dealer.Role),
            new Claim("FullName", $"{dealer.FirstName} {dealer.LastName}"),
        };

        return claims;
    }

    public string CreateRefreshToken()
    {
        return Guid.NewGuid().ToString();
    }
}
