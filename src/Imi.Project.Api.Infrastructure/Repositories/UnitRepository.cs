using Imi.Project.Api.Core.Entities;
using Imi.Project.Api.Core.Enums;
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
    public class UnitRepository : BaseRepository<Unit>, IUnitRepository
    {
        public UnitRepository(ApplicationDbContext dbContext) : base(dbContext) {}

        public async Task<Unit> GetByNameAsync(string name)
        {
            var values = Enum.GetNames(typeof(UnitType));

            var stringResult = values.FirstOrDefault(v => v.Trim().ToLower().Equals(name.Trim().ToLower()));
            if (stringResult == null) return null;

            UnitType result = (UnitType)Enum.Parse(typeof(UnitType), stringResult, true);
            return await GetAll().FirstOrDefaultAsync(u => u.Name.Equals(result));    
            
        }
    }
}
