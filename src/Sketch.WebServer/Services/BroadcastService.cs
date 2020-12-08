using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.SignalR;

using Sketch.Shared.Data;
using Sketch.WebServer;
using Sketch.WebServer.Hubs;

namespace Sketch.WebServer.Services
{
    public class BroadcastService : IBroadcastService
    {
        private readonly IHubContext<SketchHub> _context;
        private readonly IHubConnectionMapper<User> _connections;
        private readonly IHubSubscriptionMapper<string> _subscriptions;

        public BroadcastService(
            IHubContext<SketchHub> context,
            IHubConnectionMapper<User> connections,
            IHubSubscriptionMapper<string> subscriptions)
        {
            _context = context;
            _connections = connections;
            _subscriptions = subscriptions;
        }

        public async Task PublishAsync<T>(string channel, T content)
        {
            await _context.Clients.Group(channel).SendAsync($"{content.GetType()}", content);
        }

        public async Task WhisperAsync<T>(string subscriberId, T content)
        {
            await _context.Clients.Client(subscriberId).SendAsync($"{content.GetType()}", content);
        }

        public async Task BroadcastAsync<T>(string subscriberId, T content)
        {
            await Task.WhenAll((await _subscriptions.GetSubscriptionsAsync(subscriberId)).Select(channel =>
            {
                return _context.Clients.GroupExcept(channel, subscriberId).SendAsync($"{content.GetType()}", content);
            }));
        }

        public async Task RegisterAsync(string subscriberId)
        {
            await _subscriptions.AddSubscriberAsync(subscriberId);
            await _connections.AddAsync(subscriberId, new User
            {
                Name = subscriberId, Guid = Guid.NewGuid()
            });
        }

        public async Task IdentifyAsync(string subscriberId, User user)
        {
            await _connections.AddAsync(subscriberId, user);
            await BroadcastAsync(subscriberId, user);
        }

        public async Task SubscribeAsync(string subscriberId, string channel)
        {
            await _subscriptions.SubscribeAsync(subscriberId, channel);
            await _context.Groups.AddToGroupAsync(subscriberId, channel);

            var user = await _connections.GetConnectionInfoAsync(subscriberId);
            await PublishAsync(channel, new Subscribe { Channel = channel, User = user });

            await Task.WhenAll((await _subscriptions.GetSubscribersAsync(channel)).Select(async subscriber =>
            {
                if (subscriber != subscriberId)
                {
                    user = await _connections.GetConnectionInfoAsync(subscriber);
                    await WhisperAsync(subscriberId, new Subscribe { Channel = channel, User = user });
                }
            }));
        }

        public async Task UnregisterAsync(string subscriberId)
        {
            var user = await _connections.GetConnectionInfoAsync(subscriberId);
            foreach (var channel in await _subscriptions.GetSubscriptionsAsync(subscriberId))
            {
                await _context.Groups.RemoveFromGroupAsync(subscriberId, channel);
                await PublishAsync(channel, new Unsubscribe { Channel = channel, User = user });
            }

            await _subscriptions.RemoveSubscriberAsync(subscriberId);
        }

        public async Task UnsubscribeAsync(string subscriberId, string channel)
        {
            await _subscriptions.UnsubscribeAsync(subscriberId, channel);
            await _context.Groups.RemoveFromGroupAsync(subscriberId, channel);

            var user = await _connections.GetConnectionInfoAsync(subscriberId);
            await PublishAsync(channel, new Unsubscribe { Channel = channel, User = user });

            await Task.WhenAll((await _subscriptions.GetSubscribersAsync(channel)).Select(async subscriber =>
            {
                user = await _connections.GetConnectionInfoAsync(subscriber);
                await WhisperAsync(subscriberId, new Unsubscribe { Channel = channel, User = user });
            }));
        }
    }
}
