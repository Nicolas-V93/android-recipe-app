using Imi.Project.Mobile.Interfaces;
using Imi.Project.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Imi.Project.Mobile.Services.Mock
{
    public class MockCategoryService : ICategoryService
    {
        private List<Category> mockCategories = new List<Category>()
        {
            new Category { Id = Guid.Parse("00000000-0000-0000-0000-000000000001"), Name = "Lunch" },
            new Category { Id = Guid.Parse("00000000-0000-0000-0000-000000000002"), Name = "Dinner" },
        };
        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            return await Task.FromResult(mockCategories);
        }
    }
}
