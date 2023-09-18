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
    public class IngredientRepository : BaseRepository<Ingredient>, IIngredientRepository
    {
        public IngredientRepository(ApplicationDbContext dbContext) : base(dbContext) {}

        public async Task<Ingredient> GetByNameAsync(string name)
        {
            return await GetAll().FirstOrDefaultAsync(i => i.Name.Trim().ToUpper().Equals(name.Trim().ToUpper()));
        }
    }
}
