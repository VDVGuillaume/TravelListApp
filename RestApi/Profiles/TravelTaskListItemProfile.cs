using AutoMapper;
using RestApi.Dtos;
using TravelListModels;

namespace RestApi.Profiles
{
    public class TravelTaskListItemProfile : Profile
    {
        public TravelTaskListItemProfile()
        {
            //Source -> Target
            CreateMap<TravelTaskListItem, TravelTaskListItemReadDto>();
            CreateMap<TravelTaskListItemCreateDto, TravelTaskListItem>();


        }
    }
}
