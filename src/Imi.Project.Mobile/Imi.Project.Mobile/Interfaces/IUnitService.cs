using Imi.Project.Mobile.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Imi.Project.Mobile.Interfaces
{
    public interface IUnitService
    {
        Task<IEnumerable<Unit>> GetAllUnits();
    }
}
