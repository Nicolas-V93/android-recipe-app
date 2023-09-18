using Imi.Project.Mobile.Interfaces;
using Imi.Project.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Imi.Project.Mobile.Services.Mock
{
    public class MockDietService : IDietService
    {
        private List<Diet> mockDiets = new List<Diet>()
        {
            new Diet { Id = Guid.Parse("00000000-0000-0000-0000-000000000001"), Name = "Vegan" },
            new Diet { Id = Guid.Parse("00000000-0000-0000-0000-000000000002"), Name = "Vegetarian" },
        };

        public async Task<IEnumerable<Diet>> GetAllDiets()
        {
            return await Task.FromResult(mockDiets);
        }
    }
}
