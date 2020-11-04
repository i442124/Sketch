using System.Threading;
using System.Threading.Tasks;

using Sketch;
using Sketch.Shared;
using Sketch.WebServer.Hubs;

namespace Sketch.WebServer.Services
{
    public interface INotificationService
    {
        Task PublishAsync<T>(string channel, T content);

        Task WhisperAsync<T>(string subscriberId, T content);

        Task UpdateAsync(string subscriberId, User user);

        Task RegisterAsync(string subscriberId, User user);

        Task SubscribeAsync(string subscriberId, string channel);

        Task UnregisterAsync(string subscriberId);

        Task UnsubscribeAsync(string subscriberId, string channel);
    }
}
