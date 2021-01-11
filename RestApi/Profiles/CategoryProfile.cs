using AutoMapper;
using RestApi.Dtos;
using TravelListModels;

namespace TravelList.Api.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryReadDto>();
            CreateMap<CategoryCreateDto, Category>();
        }

    }
}
