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
        private const string CALLBACK_METHOD = "ReceiveAsync";

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

        public async Task PublishAsync<T>(string channel, T content)
        {
            var methodName = $"{CALLBACK_METHOD}_{typeof(T)}";
            await _context.Clients.Group(channel).SendAsync(methodName, content);
        }

        public async Task RegisterAsync(string subscriberId, User user)
        {
            await _connections.AddAsync(subscriberId, user);
            await _subscriptions.AddSubscriberAsync(subscriberId);
        }

        public async Task SubscribeAsync(string subscriberId, string channel)
        {
            await _subscriptions.SubscribeAsync(subscriberId, channel);
            await _context.Groups.AddToGroupAsync(subscriberId, channel);

            var user = await _connections.GetUserInfoAsync(subscriberId);
            var userEvent = new UserEvent { User = user, TimeStamp = DateTime.Now, Connected = true };

            await PublishAsync(channel, userEvent);
        }

        public async Task UnregisterAsync(string subscriberId)
        {
            await _connections.RemoveAsync(subscriberId);
            await _subscriptions.RemoveSubscriberAsync(subscriberId);
        }

        public async Task UnsubscribeAsync(string subscriberId, string channel)
        {
            await _subscriptions.UnsubscribeAsync(subscriberId, channel);
            await _context.Groups.RemoveFromGroupAsync(subscriberId, channel);

            var user = await _connections.GetUserInfoAsync(subscriberId);
            var userEvent = new UserEvent { User = user, TimeStamp = DateTime.Now, Connected = false };

            await PublishAsync(channel, userEvent);
        }
    }
}
