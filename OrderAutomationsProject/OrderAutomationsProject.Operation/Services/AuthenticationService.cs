using AutoMapper;
using Azure.Core;
using MediatR;
using OrderAutomationsProject.Base.Encryption;
using OrderAutomationsProject.Base.Response;
using OrderAutomationsProject.Data.Domain;
using OrderAutomationsProject.Data.UnitOfWorks;
using OrderAutomationsProject.Operation.Cqrs;
using OrderAutomationsProject.Schema;

namespace OrderAutomationsProject.Operation.Services;

public class AuthenticationServiceCommandHandler : IRequestHandler<AuthenticationServiceCommand, ApiResponse<LoginResponse>>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    private readonly IMediator mediator;
    
    public AuthenticationServiceCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IMediator mediator)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
        this.mediator = mediator;
    }

    public async Task<ApiResponse<LoginResponse>> Handle(AuthenticationServiceCommand request, CancellationToken cancellationToken)
    {
        Dealer dealer = unitOfWork.DealerRepository.FirstOrDefault(x => x.Email == request.Model.Email);
        var authPass = Md5.Create(request.Model.Password).ToLower();

        if (dealer == null)
        {
            return new ApiResponse<LoginResponse>("User information is incorrect!");
        }

        if (dealer.PasswordRetryCount >= 5)
        {
            return new ApiResponse<LoginResponse>("You entered your password incorrectly 5 times. Please contact the admin to open your account!");
        }

        if (dealer.PasswordHash != authPass)
        {
            dealer.PasswordRetryCount++;
            unitOfWork.DealerRepository.Update(dealer);
            await unitOfWork.CompleteAsync(cancellationToken);
            return new ApiResponse<LoginResponse>("User information is incorrect!");
        }

        TokenRequest tokenrequest = new TokenRequest
        {
            Email = dealer.Email,
            Password = request.Model.Password
        };

        var operation = new CreateTokenCommand(tokenrequest);
        var result = await mediator.Send(operation);

        dealer.PasswordRetryCount = 0;
        unitOfWork.DealerRepository.Update(dealer);

        LoginResponse loginResponse = new LoginResponse
        {
            dealer = dealer,
            Token = result.Response.Token
        };

        await unitOfWork.CompleteAsync(cancellationToken);

        return new ApiResponse<LoginResponse>(loginResponse);
    }
}
