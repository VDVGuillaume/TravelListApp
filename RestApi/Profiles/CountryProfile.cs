using AutoMapper;
using RestApi.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelListModels;

namespace RestApi.Profiles
{
    public class CountryProfile : Profile
    {
        public CountryProfile()
        {
            //Source -> Target
            CreateMap<Country, CountryReadDto>();
        }
    }
}
