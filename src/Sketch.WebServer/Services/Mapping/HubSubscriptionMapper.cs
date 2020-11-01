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
            if (!_subscribers.TryAdd(subscriberId, new HashSet<T>()))
            {
                throw new ArgumentException("Subscriber already exists.");
            }
        }

        public Task AddSubscriberAsync(string subscriberId)
        {
            return Task.Run(() => AddSubscriber(subscriberId));
        }

        public void RemoveSubscriber(string subscriberId)
        {
            if (!_subscribers.TryRemove(subscriberId, out HashSet<T> subscriptions))
            {
                throw new ArgumentException("Subscriber does not exists.");
            }

            foreach (var subscription in subscriptions)
            {
                Unsubscribe(subscriberId, subscription);
            }
        }

        public Task RemoveSubscriberAsync(string subscriberId)
        {
            return Task.Run(() => RemoveSubscriber(subscriberId));
        }

        public void Subscribe(string subscriberId, T subscription)
        {
            if (!_subscribers.TryGetValue(subscriberId, out HashSet<T> subscriptions))
            {
                throw new ArgumentException("Subscriber does not exists.");
            }
            else if (subscriptions.Add(subscription))
            {
                throw new ArgumentException("Subscriber is already subscribed to the subscription.");
            }

            _subscriptions[subscription]++;
        }

        public Task SubscribeAsync(string subscriberId, T subscription)
        {
            return Task.Run(() => Subscribe(subscriberId, subscription));
        }

        public void Unsubscribe(string subscriberId, T subscription)
        {
            if (!_subscribers.TryGetValue(subscriberId, out HashSet<T> subscriptions))
            {
                throw new ArgumentException("Subscriber does not exists.");
            }
            else if (subscriptions.Remove(subscription))
            {
                throw new ArgumentNullException("Subscriber is not subscribed to the subscription.");
            }

            if (--_subscriptions[subscription] == 0)
            {
                _subscriptions.TryRemove(subscription, out _);
            }
        }

        public Task UnsubscribeAsync(string subscriberId, T subscription)
        {
            return Task.Run(() => Unsubscribe(subscriberId, subscription));
        }

        public IEnumerable<T> GetSubscriptions(string subscriberId)
        {
            if (!_subscribers.TryGetValue(subscriberId, out HashSet<T> subscriptions))
            {
                throw new ArgumentException("Subscriber does not exists.");
            }

            foreach (var subscription in subscriptions)
            {
                yield return subscription;
            }
        }

        public Task<IEnumerable<T>> GetSubscriptionsAsync(string subscriberId)
        {
            return Task.Run(() => GetSubscriptions(subscriberId));
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var subscription in _subscriptions.Keys)
            {
                yield return subscription;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            foreach (var subscription in _subscriptions.Keys)
            {
                yield return subscription;
            }
        }
    }
}
