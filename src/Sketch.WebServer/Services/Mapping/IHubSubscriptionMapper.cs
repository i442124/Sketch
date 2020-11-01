using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Sketch.WebServer.Services
{
    public interface IHubSubscriptionMapper<T> : IEnumerable<T>
    {
        void AddSubscriber(string subscriberId);

        Task AddSubscriberAsync(string subscriberId);

        void Subscribe(string subscriberId, T subscription);

        Task SubscribeAsync(string subscriberId, T subscription);

        void RemoveSubscriber(string subscriberId);

        Task RemoveSubscriberAsync(string subscriberId);

        void Unsubscribe(string subscriberId, T subscription);

        Task UnsubscribeAsync(string subscriberId, T subscription);

        IEnumerable<T> GetSubscriptions(string subscriberId);

        Task<IEnumerable<T>> GetSubscriptionsAsync(string subscriberId);
    }
}
