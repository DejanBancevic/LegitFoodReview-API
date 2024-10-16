using AutoMapper;
using LegitFoodReviewApp.Dto;
using LegitFoodReviewApp.Models;

namespace LegitFoodReviewApp.Helper
{
    public class MappingProfiles : Profile // koristi se za mapiranje podataka, gde mozemo preko Dto da filtriramo sta se vraca
    {
        public MappingProfiles()
        {
            CreateMap<Food, FoodDto>();
            CreateMap<FoodDto, Food>();
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>(); // kada radis update ili create mora Dto da bude prvi clan
            CreateMap<Country, CountryDto>();
            CreateMap<CountryDto, Country>();
            CreateMap<Owner, OwnerDto>();
            CreateMap<OwnerDto, Owner>();
            CreateMap<Review, ReviewDto>();
            CreateMap<ReviewDto, Review>();
            CreateMap<Reviewer, ReviewerDto>();
            CreateMap<ReviewerDto, Reviewer>();
        }
    }
}
