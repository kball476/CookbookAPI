using AutoMapper;
using cookbook3.DTO;
using cookbook3.Models;

namespace cookbook3.Helper
{

    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Recipe, RecipeDTO>();
            CreateMap<Category, CategoryDTO>();
            CreateMap<Owner, OwnerDTO>();
            CreateMap<Review, ReviewDTO>();
            CreateMap<Reviewer, ReviewerDTO>();
        }
    }
}
