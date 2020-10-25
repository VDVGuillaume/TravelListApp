using AutoMapper;
using RestApi.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelListModels;

namespace RestApi.Profiles
{
    public class TravelListProfile : Profile
    {
        public TravelListProfile()
        {
            //Source -> Target
            CreateMap<TravelListItem, TravelListReadDto>();
            CreateMap<TravelListCreateDto, TravelListItem>();
            CreateMap<TravelListUpdateDto, TravelListItem>();
            CreateMap<TravelListItem, TravelListUpdateDto>();
        }
    }
}
