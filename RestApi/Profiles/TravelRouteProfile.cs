using AutoMapper;
using RestApi.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelListModels;

namespace RestApi.Profiles
{
    public class TravelRouteProfile : Profile
    {
        public TravelRouteProfile()
        {
            //Source -> Target
            CreateMap<TravelRoute, TravelRouteReadDto>();
            CreateMap<TravelRouteCreateDto, TravelRoute>();
        }
    }
}
