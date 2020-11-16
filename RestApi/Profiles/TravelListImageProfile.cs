using AutoMapper;
using RestApi.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelListModels;

namespace RestApi.Profiles
{
    public class TravelListImageProfile : Profile
    {
        public TravelListImageProfile()
        {
            //Source -> Target
            CreateMap<TravelListItemImage, TravelListImageReadDto>();
            CreateMap<TravelListImageCreateDto, TravelListItemImage>();
        }
    }
}
