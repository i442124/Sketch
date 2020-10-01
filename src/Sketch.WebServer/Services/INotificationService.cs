using System.Threading.Tasks;

namespace Sketch.WebServer.Services
{
    public interface INotificationService
    {
        Task PublishAsync<T>(string channel, T content);

        Task SubscribeAsync(string subscriberId, string channel);

        Task UnsubscribeAsync(string subscriberId, string channel);
    }
}
