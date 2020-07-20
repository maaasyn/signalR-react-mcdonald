using AutoMapper;
using signalR_api.Dtos;
using signalR_api.Models;

namespace signalR_api.Helpers
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Order, OrderForDisplayDto>(); 
            CreateMap<Order, OrderForKitchenDto>(); 
        }
    }
}