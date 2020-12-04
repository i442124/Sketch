using System.Threading.Tasks;

namespace Sketch.WebServer.Services
{
    public interface IBroadcastService
    {
        Task WhisperAsync<T>(string subscriberId, T content);

        Task BroadcastAsync<T>(string subscriberId, T content);

        Task RegisterAsync(string subscriberId);

        Task SubscribeAsync(string subscriberId, string channel);

        Task UnregisterAsync(string subscriberId);

        Task UnsubscribeAsync(string subscriberId, string channel);
    }
}
