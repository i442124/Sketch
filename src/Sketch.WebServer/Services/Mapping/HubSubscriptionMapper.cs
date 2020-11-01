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
        private readonly ConcurrentDictionary<T, int> _subscriptions =
        new ConcurrentDictionary<T, int>();

        private readonly ConcurrentDictionary<string, HashSet<T>> _subscribers =
        new ConcurrentDictionary<string, HashSet<T>>();

        public int SubscriberCount
        {
            get { return _subscribers.Count; }
        }

        public int SubscriptionCount
        {
            get { return _subscriptions.Count; }
        }

        public void AddSubscriber(string subscriberId)
        {
            if (!_subscribers.ContainsKey(subscriberId))
            {
                _subscribers[subscriberId] = new HashSet<T>();
            }
        }

        public Task AddSubscriberAsync(string subscriberId)
        {
            return Task.Run(() => AddSubscriber(subscriberId));
        }

        public void RemoveSubscriber(string subscriberId)
        {
            if (_subscribers.TryRemove(subscriberId, out HashSet<T> set))
            {
                foreach (var subscription in set)
                {
                    _subscriptions[subscription]--;
                }
            }
        }

        public Task RemoveSubscriberAsync(string subscriberId)
        {
            return Task.Run(() => RemoveSubscriber(subscriberId));
        }

        public void Subscribe(string subscriberId, T value)
        {
            if (_subscribers[subscriberId].Add(value))
            {
                _subscriptions[value]++;
            }
        }

        public Task SubscribeAsync(string subscriberId, T value)
        {
            return Task.Run(() => Subscribe(subscriberId, value));
        }

        public void Unsubscribe(string subscriberId, T value)
        {
            if (_subscribers[subscriberId].Remove(value))
            {
                _subscriptions[value]--;
            }
        }

        public Task UnsubscribeAsync(string subscriberId, T value)
        {
            return Task.Run(() => Unsubscribe(subscriberId, value));
        }

        public IEnumerable<T> GetSubscriptions(string subscriberId)
        {
            if (_subscribers.TryGetValue(subscriberId, out HashSet<T> value))
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

        public IEnumerator<T> GetEnumerator()
        {
            return _subscriptions.Keys.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _subscriptions.Keys.GetEnumerator();
        }
    }
}
