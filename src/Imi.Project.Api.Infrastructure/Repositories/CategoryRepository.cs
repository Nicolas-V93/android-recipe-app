using Imi.Project.Api.Core.Dto.Category;
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
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext dbContext) : base(dbContext) {}

        public async Task<Category> GetByNameAsync(string name)
        {
            return await GetAll().FirstOrDefaultAsync(c => c.Name.Trim().ToUpper().Equals(name.Trim().ToUpper()));
        }

        public override async Task<Category> GetByIdAsync(Guid id)
        {
            return await GetAll().Include(c => c.Recipes).FirstOrDefaultAsync(c => c.Id.Equals(id));
        }
    }
}
