using AutoMapper;
using Imi.Project.Api.Core.Dto.Instruction;
using Imi.Project.Api.Core.Dto.Recipe;
using Imi.Project.Api.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imi.Project.Api.Core.Mapping.Profiles
{
    public class InstructionProfile : Profile
    {
        public InstructionProfile()
        {
            CreateMap<InstructionRequestDto, Instruction>()
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description.Trim()));
            CreateMap<Instruction, InstructionResponseDto>();

        }

        public static Instruction ToEntityOnAdd(InstructionRequestDto requestDto, Guid recipeId)
        {
            return new Instruction
            {
                RecipeId = recipeId,
                Description = requestDto.Description.Trim(),
                StepNumber = requestDto.StepNumber,
            };
        }

    }
}