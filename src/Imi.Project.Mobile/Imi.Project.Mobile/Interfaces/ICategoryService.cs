using Imi.Project.Mobile.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Imi.Project.Mobile.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllCategories();
    }
}
