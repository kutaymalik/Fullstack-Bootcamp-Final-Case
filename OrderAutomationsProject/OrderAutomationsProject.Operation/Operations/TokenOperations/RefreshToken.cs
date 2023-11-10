using AutoMapper;
using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OrderAutomationsProject.Base.Response;
using OrderAutomationsProject.Base.Token;
using OrderAutomationsProject.Data.Domain;
using OrderAutomationsProject.Data.UnitOfWorks;
using OrderAutomationsProject.Operation.Cqrs;
using OrderAutomationsProject.Schema;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OrderAutomationsProject.Operation.Operations.TokenOperations;

public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, ApiResponse<TokenResponse>>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    private readonly JwtConfig jwtConfig;

    public RefreshTokenCommandHandler(IUnitOfWork unitOfWork, IOptionsMonitor<JwtConfig> jwtConfig, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.jwtConfig = jwtConfig.CurrentValue;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<TokenResponse>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var dealer = unitOfWork.DealerRepository.FirstOrDefault(x => x.RefreshToken == request.RefreshToken && x.RefreshTokenExpireDate > DateTime.Now);

        if(dealer != null)
        {
            var token = Token(dealer);

            TokenResponse tokenResponse = new()
            {
                Token = token,
                ExpireDate = DateTime.Now.AddMinutes(jwtConfig.AccessTokenExpiration),
                DealerNumber = dealer.DealerNumber,
                Email = dealer.Email,
                RefreshToken = CreateRefreshToken(),
                FirstName = dealer.FirstName,
                LastName = dealer.LastName,
                DealerId = dealer.Id,
                Role = dealer.Role,
            };

            dealer.RefreshToken = tokenResponse.RefreshToken;
            dealer.RefreshTokenExpireDate = tokenResponse.ExpireDate;
            unitOfWork.DealerRepository.Update(dealer);
            await unitOfWork.CompleteAsync(cancellationToken);

            return new ApiResponse<TokenResponse>(tokenResponse);
        }
        else
        {
            throw new InvalidOperationException("No valid refresh token found!");
        }
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
