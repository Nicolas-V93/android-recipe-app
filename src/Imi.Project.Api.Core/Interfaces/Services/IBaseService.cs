using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Imi.Project.Api.Core.Interfaces.Services
{
    public interface IBaseService<T, TRequest>
    {
        Task<IEnumerable<T>> ListAllAsync();
        Task<T> GetByIdAsync(Guid id);
        Task<T> AddAsync(TRequest requestDto);
        Task UpdateAsync(Guid id, TRequest requestDto);
        Task DeleteAsync(Guid id);
        Task<bool> EntityExistsAsync(Guid id);
    }
}
