using AutoMapper;
using Imi.Project.Api.Core.Dto.Category;
using Imi.Project.Api.Core.Dto.Unit;
using Imi.Project.Api.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imi.Project.Api.Core.Mapping.Profiles
{
    public class UnitProfile : Profile
    {
        public UnitProfile()
        {
            CreateMap<Unit, UnitResponseDto>();
            CreateMap<UnitRequestDto, Unit>();
        }
    }
}
