using MediatR;
using OrderAutomationsProject.Base.Response;
using OrderAutomationsProject.Schema;

namespace OrderAutomationsProject.Operation.Cqrs;

public record CreateTokenCommand(TokenRequest Model) : IRequest<ApiResponse<TokenResponse>>;
public record RefreshTokenCommand(string RefreshToken) : IRequest<ApiResponse<TokenResponse>>;