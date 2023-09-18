using AutoMapper;
using Imi.Project.Api.Core.Dto.RecipeIngredient;
using Imi.Project.Api.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imi.Project.Api.Core.Mapping.Profiles
{
    public class RecipeIngredientProfile : Profile
    {
        public RecipeIngredientProfile()
        {
            CreateMap<RecipeIngredient, RecipeIngredientResponseDto>()
                .ForMember(dest => dest.Ingredient, opt => opt.MapFrom(src => src.Ingredient.Name))
                .ForMember(dest => dest.Unit, opt => opt.MapFrom(src => src.Unit.Name.ToString()))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.IngredientId));

        }

        public static RecipeIngredient ToEntity(Guid recipeId, Guid ingredientId, Guid unitId, double amount)
        {
            return new RecipeIngredient
            {
                RecipeId = recipeId,
                IngredientId = ingredientId,
                Amount = amount,
                UnitId = unitId,
            };
        }

        public static void Update(RecipeIngredient entity, Guid unitId, double amount)
        {
            entity.UnitId = unitId;
            entity.Amount = amount;
        }

    }
}
