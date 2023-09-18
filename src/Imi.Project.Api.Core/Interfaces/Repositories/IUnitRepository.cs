using Imi.Project.Api.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imi.Project.Api.Core.Interfaces.Repositories
{
    public interface IUnitRepository : IBaseRepository<Unit>
    {
        Task<Unit> GetByNameAsync(string name);
    }
}
