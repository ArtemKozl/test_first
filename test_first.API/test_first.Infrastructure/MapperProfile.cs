using AutoMapper;
using test_first.Core.Models;
using test_first.DataAccess.Entities;

namespace test_first.Infrastructure
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<DeliveryEntity, Deliveries>();
        }
    }
}
