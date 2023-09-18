using AutoMapper;
using Imi.Project.Api.Core.Dto.Category;
using Imi.Project.Api.Core.Dto.Ingredient;
using Imi.Project.Api.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imi.Project.Api.Core.Mapping.Profiles
{
    public class IngredientProfile : Profile
    {
        public IngredientProfile()
        {
            CreateMap<Ingredient, IngredientResponseDto>();
            CreateMap<IngredientRequestDto, Ingredient>()
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.Trim().ToLower()));

        }
    }
}
