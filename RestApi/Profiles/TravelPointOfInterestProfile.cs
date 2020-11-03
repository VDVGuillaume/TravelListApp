using AutoMapper;
using RestApi.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelListModels;

namespace RestApi.Profiles
{
    public class TravelPointOfInterestProfile : Profile
    {
        public TravelPointOfInterestProfile()
        {
            //Source -> Target
            CreateMap<TravelPointOfInterest, TravelPointOfInterestReadDto>();
            CreateMap<TravelPointOfInterestCreateDto, TravelPointOfInterest>();
        }
    }
}
