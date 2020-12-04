using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.SignalR;

using Sketch.Shared.Data;
using Sketch.Shared.Data.Users;

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
        }

        public async Task SubscribeAsync(string subscriberId, string channel)
        {
            await _subscriptions.SubscribeAsync(subscriberId, channel);
            await _context.Groups.AddToGroupAsync(subscriberId, channel);
        }

        public async Task UnregisterAsync(string subscriberId)
        {
            foreach (var channel in await _subscriptions.GetSubscriptionsAsync(subscriberId))
            {
                await _context.Groups.RemoveFromGroupAsync(subscriberId, channel);
            }

            await _subscriptions.RemoveSubscriberAsync(subscriberId);
        }

        public async Task UnsubscribeAsync(string subscriberId, string channel)
        {
            await _subscriptions.UnsubscribeAsync(subscriberId, channel);
            await _context.Groups.RemoveFromGroupAsync(subscriberId, channel);
        }
    }
}
