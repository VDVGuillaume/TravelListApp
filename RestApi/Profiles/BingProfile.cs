using AutoMapper;
using RestApi.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelListModels;

namespace RestApi.Profiles
{
    public class BingProfile : Profile
    {
        public BingProfile()
        {
            //Source -> Target
            CreateMap<Location, LocationReadDto>();
            CreateMap<Resources, ResourcesReadDto>();
            CreateMap<Resource, ResourceReadDto>();
            CreateMap<GeoPoint, GeoPointReadDto>();
        }
    }
}
