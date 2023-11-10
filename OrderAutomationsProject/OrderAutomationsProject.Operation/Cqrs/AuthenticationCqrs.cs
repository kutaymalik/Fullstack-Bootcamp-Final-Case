using MediatR;
using OrderAutomationsProject.Base.Response;
using OrderAutomationsProject.Schema;

namespace OrderAutomationsProject.Operation.Cqrs;

public record AuthenticationServiceCommand(LoginRequest Model) : IRequest<ApiResponse<LoginResponse>>;