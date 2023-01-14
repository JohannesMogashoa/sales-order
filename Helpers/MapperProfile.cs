using AutoMapper;
using SalesOrder.Models;
using SalesOrder.Models.RestModels;

namespace SalesOrder.Helpers
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<OrderHeader, OrderHeaderRestModel>();
            CreateMap<OrderLine, OrderLineRestModel>();
        }
    }
}
