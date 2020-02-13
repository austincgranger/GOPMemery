using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GOPMemery.Services
{
    public interface IDataStore<T>
    {
        Task<bool> AddUserAsync(T item);
        Task<bool> UpdateUserAsync(T item);
        Task<bool> DeleteUserAsync(string id);
        Task<T> GetUserAsync(string id);
        
        // eh
        Task<IEnumerable<T>> GetUsersAsync(bool forceRefresh = false);
    }
}
