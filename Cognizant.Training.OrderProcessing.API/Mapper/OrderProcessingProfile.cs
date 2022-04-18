using AutoMapper;
using Cognizant.Training.OrderProcessing.API.Domain;
using Cognizant.Training.OrderProcessing.API.Dto;

namespace Cognizant.Training.OrderProcessing.API.Mapper
{
    public class OrderProcessingProfile : Profile
    {
        public OrderProcessingProfile()
        {
            CreateMap<OrderLineItemDto, OrderLineItem>().ReverseMap();
            CreateMap<Order, OrderDto>();
            CreateMap<CreateOrderDto, Order>()
                .ForMember(x => x.Id, opt => opt.MapFrom(_ => Guid.NewGuid()))
                .ForMember(x => x.CustomerName, opt => opt.MapFrom(x => x.CustomerName))
                .ForMember(x => x.AddressLine1, opt => opt.MapFrom(x => x.AddressLine1))
                .ForMember(x => x.AddressLine2, opt => opt.MapFrom(x => x.AddressLine2))
                .ForMember(x => x.City, opt => opt.MapFrom(x => x.City))
                .ForMember(x => x.PostCode, opt => opt.MapFrom(x => x.PostCode))
                .ForMember(x => x.LineItems, opt => opt.MapFrom(x => x.LineItems));
        }
    }
}
