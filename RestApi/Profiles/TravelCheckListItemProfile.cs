﻿using AutoMapper;
using RestApi.Dtos;
using TravelListModels;

namespace RestApi.Profiles
{
    public class TravelCheckListItemProfile : Profile
    {
        public TravelCheckListItemProfile()
        {
            //Source -> Target
            CreateMap<TravelCheckListItem, TravelCheckListItemReadDto>();
            CreateMap<TravelCheckListItemCreateDto, TravelCheckListItem>();

        }
    }
}
