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
    public class DietRepository : BaseRepository<Diet>, IDietRepository
    {
        public DietRepository(ApplicationDbContext dbContext) : base(dbContext) {}

        public async Task<Diet> GetByNameAsync(string name)
        {
            return await GetAll().FirstOrDefaultAsync(d => d.Name.Trim().ToUpper().Equals(name.Trim().ToUpper()));
        }

        public override async Task<Diet> GetByIdAsync(Guid id)
        {
            return await GetAll().Include(d => d.Recipes).FirstOrDefaultAsync(d => d.Id.Equals(id));
        }
    }
}
