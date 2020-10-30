using System.Threading.Tasks;

namespace Sketch.WebServer.Services
{
    public interface IHubConnectionMapper<T>
    {
        Task RemoveAsync(string connectionId);

        Task AddAsync(string connectionId, T value);

        Task<T> GetUserInfoAsync(string connectionId);
    }
}
