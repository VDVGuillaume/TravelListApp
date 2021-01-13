using AutoMapper;
using RestApi.Dtos;
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
