using MediatR;
using OrderAutomationsProject.Base.Response;
using OrderAutomationsProject.Schema;

namespace OrderAutomationsProject.Operation.Cqrs;

public record CreateAddressCommand(AddressRequest Model) : IRequest<ApiResponse<AddressResponse>>;
public record UpdateAddressCommand(AddressRequest Model, int Id) : IRequest<ApiResponse>;
public record DeleteAddressCommand(int Id) : IRequest<ApiResponse>;


public record GetAllAddressQuery() : IRequest<ApiResponse<List<AddressResponse>>>;
public record GetAddressByIdQuery(int Id) : IRequest<ApiResponse<AddressResponse>>;
public record GetAddressByDealerIdQuery(int DealerId) : IRequest<ApiResponse<List<AddressResponse>>>;