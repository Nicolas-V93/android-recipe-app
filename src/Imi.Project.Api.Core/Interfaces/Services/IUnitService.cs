using Imi.Project.Api.Core.Dto.Unit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Imi.Project.Api.Core.Interfaces.Services
{
    public interface IUnitService
    {
        Task<IEnumerable<UnitResponseDto>> ListAllAsync();
        Task<UnitResponseDto> GetByIdAsync(Guid id);
        Task<bool> EntityExistsAsync(Guid id);
    }
}
