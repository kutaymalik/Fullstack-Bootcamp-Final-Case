using MediatR;
using OrderAutomationsProject.Base.Response;
using OrderAutomationsProject.Schema;

namespace OrderAutomationsProject.Operation.Cqrs;

public record CreateCardCommand(CardRequest Model) : IRequest<ApiResponse<CardResponse>>;
public record UpdateCardCommand(UpdateCardRequest Model, int Id) : IRequest<ApiResponse>;
public record DeleteCardCommand(int Id) : IRequest<ApiResponse>;


public record GetAllCardQuery() : IRequest<ApiResponse<List<CardResponse>>>;
public record GetCardByIdQuery(int Id) : IRequest<ApiResponse<CardResponse>>;
public record GetCardByDealerIdQuery(int DealerId) : IRequest<ApiResponse<List<CardResponse>>>;

