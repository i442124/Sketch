using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Sketch.WebServer.Services
{
    public class HubSubscriptionMapper<T> : IHubSubscriptionMapper<T>
    {
        private readonly ConcurrentDictionary<string, List<T>> _subscribers =
        new ConcurrentDictionary<string, List<T>>();

        public int SubscriberCount
        {
            get { return _subscribers.Count; }
        }

        public void AddSubscriber(string subscriberId)
        {
            _subscribers[subscriberId] = new List<T>();
        }

        public Task AddSubscriberAsync(string subscriberId)
        {
            return Task.Run(() => AddSubscriber(subscriberId));
        }

        public void RemoveSubscriber(string subscriberId)
        {
            _subscribers.TryRemove(subscriberId, out _);
        }

        public Task RemoveSubscriberAsync(string subscriberId)
        {
            return Task.Run(() => RemoveSubscriber(subscriberId));
        }

        public void Subscribe(string subscriberId, T value)
        {
            _subscribers[subscriberId].Add(value);
        }

        public Task SubscribeAsync(string subscriberId, T value)
        {
            return Task.Run(() => Subscribe(subscriberId, value));
        }

        public void Unsubscribe(string subscriberId, T value)
        {
            _subscribers[subscriberId].Remove(value);
        }

        public Task UnsubscribeAsync(string subscriberId, T value)
        {
            return Task.Run(() => Unsubscribe(subscriberId, value));
        }

        public IEnumerable<T> GetSubscriptions(string subscriberId)
        {
            if (_subscribers.TryGetValue(subscriberId, out List<T> value))
            {
                foreach (var item in value)
                {
                    yield return item;
                }
            }

            yield break;
        }

        public Task<IEnumerable<T>> GetSubscriptionsAsync(string subscriberId)
        {
            return Task.Run(() => GetSubscriptions(subscriberId));
        }
    }
}
