using AutoMapper;
using Imi.Project.Api.Core.Dto.Category;
using Imi.Project.Api.Core.Dto.Diet;
using Imi.Project.Api.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imi.Project.Api.Core.Mapping.Profiles
{
    public class DietProfile : Profile
    {
        public DietProfile()
        {
            CreateMap<Diet, DietResponseDto>();
            CreateMap<DietRequestDto, Diet>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.Trim().ToLower()));
        }
    }
}
