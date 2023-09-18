using Imi.Project.Api.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imi.Project.Api.Core.Interfaces.Services
{
    public interface IRoleService
    {
        Task<Role> GetByIdAsync(Guid id);
        Task<bool> RoleExistsAsync(Guid id);
    }
}
