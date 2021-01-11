using AutoMapper;
using RestApi.Dtos;
using TravelListModels;

namespace TravelList.Api.Profiles
{
    public class TravelCheckListItemProfile : Profile
    {
        public TravelCheckListItemProfile()
        {
            //Source -> Target
            CreateMap<TravelCheckListItem, ChecklistItemReadDto>();
            CreateMap<TravelCheckListItemCreateDto, TravelCheckListItem>();

        }
    }
}
