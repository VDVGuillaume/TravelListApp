using AutoMapper;
using RestApi.Dtos;
using TravelListModels;

namespace RestApi.Profiles
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
