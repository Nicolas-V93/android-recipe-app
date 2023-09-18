using Imi.Project.Api.Core.Entities;
using Imi.Project.Api.Core.Interfaces.Repositories;
using Imi.Project.Api.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imi.Project.Api.Infrastructure.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public RoleRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Role> GetByIdAsync(Guid id)
        {
            return await _dbContext.Set<Role>().FirstOrDefaultAsync(x => x.Id.Equals(id));
        }

        public async Task<bool> RoleExistsAsync(Guid id)
        {
            return await _dbContext.Set<Role>().AnyAsync(r => r.Id.Equals(id));
        }
    }
}
