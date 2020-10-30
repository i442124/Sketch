using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Sketch.WebServer.Services
{
    public interface IHubSubscriptionMapper<T>
    {
        Task AddSubscriberAsync(string subscriberId);

        Task SubscribeAsync(string subscriberId, T value);

        Task RemoveSubscriberAsync(string subscriberId);

        Task UnsubscribeAsync(string subscriberId, T value);

        Task<IEnumerable<T>> GetSubscriptionsAsync(string subscriberId);
    }
}
