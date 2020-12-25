using AutoMapper;
using RestApi.Dtos;
using TravelListModels;

namespace TravelList.Api.Profiles
{
    public class CheckListItemProfile : Profile
    {
        public CheckListItemProfile()
        {
            //Source -> Target
            CreateMap<CheckListItem, ChecklistItemReadDto>();
            CreateMap<CheckListItemCreateDto, CheckListItem>();
        }
    }
}
