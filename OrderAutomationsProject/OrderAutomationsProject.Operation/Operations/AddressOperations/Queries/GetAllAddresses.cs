using AutoMapper;
using MediatR;
using OrderAutomationsProject.Base.Response;
using OrderAutomationsProject.Data.Domain;
using OrderAutomationsProject.Data.UnitOfWorks;
using OrderAutomationsProject.Operation.Cqrs;
using OrderAutomationsProject.Schema;

namespace OrderAutomationsProject.Operation.Operations.AddressOperations.Queries;

public class GetAllAddressesQueryHandler : IRequestHandler<GetAllAddressQuery, ApiResponse<List<AddressResponse>>>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public GetAllAddressesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<List<AddressResponse>>> Handle(GetAllAddressQuery request, CancellationToken cancellationToken)
    {
        List<Address> list = unitOfWork.AddressRepository.GetAll().Where(x => x.IsActive == true).ToList();

        List<AddressResponse> mapped = mapper.Map<List<AddressResponse>>(list);

        return new ApiResponse<List<AddressResponse>>(mapped);
    }
}
