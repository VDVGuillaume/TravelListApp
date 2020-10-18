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
            CreateMap<TravelList, TravelListReadDto>();
            CreateMap<TravelListCreateDto, TravelList>();
            CreateMap<TravelListUpdateDto, TravelList>();
            CreateMap<TravelList, TravelListUpdateDto>();
        }
    }
}
