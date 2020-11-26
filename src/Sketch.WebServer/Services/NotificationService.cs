using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.SignalR;

using Sketch.Shared;
using Sketch.WebServer.Hubs;

namespace Sketch.WebServer.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IHubContext<SocialHub> _context;
        private readonly IHubConnectionMapper<User> _connections;
        private readonly IHubSubscriptionMapper<string> _subscriptions;

        public NotificationService(
            IHubContext<SocialHub> context,
            IHubConnectionMapper<User> connections,
            IHubSubscriptionMapper<string> subscriptions)
        {
            _context = context;
            _connections = connections;
            _subscriptions = subscriptions;
        }

        public Task PublishAsync<T>(string channel, T content)
        {
            return _context.Clients.Group(channel).SendAsync($"{content.GetType()}", content);
        }

        public Task WhisperAsync<T>(string subscriberId, T content)
        {
            return _context.Clients.Client(subscriberId).SendAsync($"{content.GetType()}", content);
        }

        public Task BroadcastAsync<T>(string channel, string subscriberId, T content)
        {
            return _context.Clients.GroupExcept(channel, subscriberId).SendAsync($"{content.GetType()}", content);
        }

        public async Task UpdateAsync(string subscriberId, User user)
        {
            await _connections.AddAsync(subscriberId, user);
            foreach (var channel in await _subscriptions.GetSubscriptionsAsync(subscriberId))
            {
                await PublishAsync(channel, await CreateUserEventAsync(subscriberId));
            }
        }

        public async Task RegisterAsync(string subscriberId, User user)
        {
            await _connections.AddAsync(subscriberId, user);
            await _subscriptions.AddSubscriberAsync(subscriberId);
        }

        public async Task SubscribeAsync(string subscriberId, string channel)
        {
            await _subscriptions.SubscribeAsync(subscriberId, channel);
            await PublishAsync(channel, await CreateSubscribeEventAsync(subscriberId, channel));

            await _context.Groups.AddToGroupAsync(subscriberId, channel);
            foreach (var subscriber in await _subscriptions.GetSubscribersAsync(channel))
            {
                var subscribeEvent = await CreateSubscribeEventAsync(subscriber, channel);
                await WhisperAsync(subscriberId, subscribeEvent);
            }
        }

        public async Task UnregisterAsync(string subscriberId)
        {
            foreach (var channel in await _subscriptions.GetSubscriptionsAsync(subscriberId))
            {
                await _context.Groups.RemoveFromGroupAsync(subscriberId, channel);
                var unsubscribeEvent = await CreateUnsubscribeEventAsync(subscriberId, channel);
                await PublishAsync(channel, unsubscribeEvent);
            }

            await _subscriptions.RemoveSubscriberAsync(subscriberId);
            await _connections.RemoveAsync(subscriberId);
        }

        public async Task UnsubscribeAsync(string subscriberId, string channel)
        {
            await _subscriptions.UnsubscribeAsync(subscriberId, channel);
            await PublishAsync(channel, await CreateUnsubscribeEventAsync(subscriberId, channel));

            await _context.Groups.RemoveFromGroupAsync(subscriberId, channel);
            foreach (var subscriber in await _subscriptions.GetSubscribersAsync(channel))
            {
                var unsubscribeEvent = await CreateUnsubscribeEventAsync(subscriber, channel);
                await WhisperAsync(subscriberId, unsubscribeEvent);
            }
        }

        private async Task<UserEvent> CreateUserEventAsync(string subscriberId)
        {
            var user = await _connections.GetUserInfoAsync(subscriberId);
            return new UserEvent { User = user, TimeStamp = DateTime.Now };
        }

        private async Task<SubscribeEvent> CreateSubscribeEventAsync(string subscriberId, string channel)
        {
            var user = await _connections.GetUserInfoAsync(subscriberId);
            return new SubscribeEvent { DateTime = DateTime.Now, Subscription = new Subscribe { Channel = channel, User = user } };
        }

        private async Task<UnsubscribeEvent> CreateUnsubscribeEventAsync(string subscriberId, string channel)
        {
            var user = await _connections.GetUserInfoAsync(subscriberId);
            return new UnsubscribeEvent { DateTime = DateTime.Now, Unsubscription = new Unsubscribe { Channel = channel, User = user } };
        }
    }
}
