using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Imi.Project.Mobile.Interfaces
{
    public interface IBaseRepository
    {
        Task<IEnumerable<T>> GetAllAsync<T>(string url, string token = "");
        Task<T> GetAsync<T>(Guid id, string url, string token = "");
        Task<T> PostAsync<T>(T entity, string url, string token = "");
        Task<TResponse> PostAsync<T, TResponse>(T entity, string url, string token = "");
        Task<T> PutAsync<T>(T entity, string url, string token = "");
        Task DeleteAsync(Guid id, string url, string token = "");
    }
}
