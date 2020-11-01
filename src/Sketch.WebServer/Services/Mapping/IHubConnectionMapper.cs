using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sketch.WebServer.Services
{
    public interface IHubConnectionMapper<T> : IEnumerable<T>
    {
        void Remove(string connectionId);

        Task RemoveAsync(string connectionId);

        void Add(string connectionId, T value);

        Task AddAsync(string connectionId, T value);

        T GetUserInfo(string connectionId);

        Task<T> GetUserInfoAsync(string connectionId);
    }
}
