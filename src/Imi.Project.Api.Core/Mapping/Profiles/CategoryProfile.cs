using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Imi.Project.Api.Core.Dto;
using Imi.Project.Api.Core.Dto.Category;
using Imi.Project.Api.Core.Entities;

namespace Imi.Project.Api.Core.Mapping.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryResponseDto>();

            CreateMap<CategoryRequestDto, Category>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.Trim().ToLower()));
        }
    }
}
