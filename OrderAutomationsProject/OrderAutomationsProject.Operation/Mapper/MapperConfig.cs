using AutoMapper;
using OrderAutomationsProject.Data.Domain;
using OrderAutomationsProject.Schema;

namespace OrderAutomationsProject.Operation.Mapper;

public class MapperConfig : Profile
{
    public MapperConfig()
    {
        CreateMap<AddressRequest, Address>();
        CreateMap<Address, AddressResponse>()
            .ForMember(dest => dest.DealerName, opt => opt.MapFrom(src => src.Dealer.FirstName + " " + src.Dealer.LastName));

        CreateMap<BillRequest, Bill>();
        CreateMap<Bill, BillResponse>();

        CreateMap<CardRequest, Card>();
        CreateMap<Card, CardResponse>();

        CreateMap<CategoryRequest, Category>();
        CreateMap<Category, CategoryResponse>();

        CreateMap<CostRequest, Cost>();
        CreateMap<Cost, CostResponse>()
            .ForMember(dest => dest.DealerName, opt => opt.MapFrom(src => src.Dealer.FirstName + " " + src.Dealer.LastName));

        CreateMap<DealerRequest, Dealer>();
        CreateMap<Dealer, DealerResponse>();

        CreateMap<ProductRequest, Product>();
        CreateMap<Product, ProductResponse>()
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));

        CreateMap<OrderRequest, Order>();
        CreateMap<Order, OrderResponse>();

        CreateMap<Payment, PaymentResponse>();

        CreateMap<Message, MessageResponse>();
    }
}
