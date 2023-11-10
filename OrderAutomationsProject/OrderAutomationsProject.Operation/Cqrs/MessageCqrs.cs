using MediatR;
using OrderAutomationsProject.Base.Response;
using OrderAutomationsProject.Schema;

namespace OrderAutomationsProject.Operation.Cqrs;

public record GetMessagesByDealerIdQuery(int DealerId) : IRequest<ApiResponse<List<MessageResponse>>>;