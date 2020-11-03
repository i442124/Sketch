using System;
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
            return _context.Clients.Group(channel).SendAsync($"{typeof(T)}", content);
        }

        public Task WhisperAsync<T>(string subscriberId, T content)
        {
            return _context.Clients.Client(subscriberId).SendAsync($"{typeof(T)}", content);
        }

        public Task BroadcastAsync<T>(string subscriberId, string channel, T content)
        {
            return _context.Clients.GroupExcept(channel, subscriberId).SendAsync($"{typeof(T)}", content);
        }

        public async Task UpdateAsync(string subscriberId, User user)
        {
            await _connections.AddAsync(subscriberId, user);
            foreach (var channel in await _subscriptions.GetSubscriptionsAsync(subscriberId))
            {
                await PublishAsync(channel, new UserEvent
                {
                    User = user, TimeStamp = DateTime.Now, Connected = true
                });
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
            await PublishAsync(channel, await CreateEvent(subscriberId, true));

            await _context.Groups.AddToGroupAsync(subscriberId, channel);
            foreach (var subscriber in await _subscriptions.GetSubscribersAsync(channel))
            {
                await WhisperAsync(subscriberId, await CreateEvent(subscriber, true));
            }
        }

        public async Task UnregisterAsync(string subscriberId)
        {
            await _connections.RemoveAsync(subscriberId);
            await _subscriptions.RemoveSubscriberAsync(subscriberId);
        }

        public async Task UnsubscribeAsync(string subscriberId, string channel)
        {
            await _subscriptions.UnsubscribeAsync(subscriberId, channel);
            await PublishAsync(channel, await CreateEvent(subscriberId, false));

            await _context.Groups.RemoveFromGroupAsync(subscriberId, channel);
            foreach (var subscriber in await _subscriptions.GetSubscribersAsync(channel))
            {
                await WhisperAsync(subscriberId, await CreateEvent(subscriber, false));
            }
        }

        private async Task<UserEvent> CreateEvent(string subscriberId, bool connected)
        {
            var user = await _connections.GetUserInfoAsync(subscriberId);
            return new UserEvent { User = user, Connected = connected, TimeStamp = DateTime.Now };
        }
    }
}
