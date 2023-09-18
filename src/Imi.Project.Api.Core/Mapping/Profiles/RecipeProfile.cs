using AutoMapper;
using Imi.Project.Api.Core.Dto.Instruction;
using Imi.Project.Api.Core.Dto.Recipe;
using Imi.Project.Api.Core.Dto.RecipeIngredient;
using Imi.Project.Api.Core.Dto.User;
using Imi.Project.Api.Core.Entities;
using System.Linq;

namespace Imi.Project.Api.Core.Mapping.Profiles
{
    public class RecipeProfile : Profile
    {
        public RecipeProfile()
        {
            CreateMap<Recipe, RecipeResponseDto>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.Diet, opt => opt.MapFrom(src => src.Diet.Name))
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => new UserResponseDto
                {
                    Id = src.ApplicationUser.Id,
                    Username = src.ApplicationUser.UserName
                }));

            CreateMap<Recipe, RecipeDetailsResponseDto>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.Diet, opt => opt.MapFrom(src => src.Diet.Name))
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => new UserResponseDto
                {
                    Id = src.ApplicationUser.Id,
                    Username = src.ApplicationUser.UserName
                }))
                .ForMember(dest => dest.Ingredients, opt => opt.MapFrom(src => src.RecipeIngredients.Select
                (ri => new RecipeIngredientResponseDto
                {
                    Id = ri.IngredientId,
                    Ingredient = ri.Ingredient.Name,
                    Amount = ri.Amount,
                    Unit = ri.Unit.Name.ToString()
                })))
                .ForMember(dest => dest.Instructions, opt => opt.MapFrom(src => src.Instructions.Select
                (i => new InstructionResponseDto
                {
                    Id = i.Id,
                    Description = i.Description,
                    StepNumber = i.StepNumber
                })));


            CreateMap<RecipeRequestDto, Recipe>()
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => new Category { Name = src.Category }))
            .ForMember(dest => dest.Diet, opt => opt.MapFrom(src => new Diet { Name = src.Diet }));
        }


    }
}
