using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.SignalR;

using Sketch.Shared;
using Sketch.WebServer;
using Sketch.WebServer.Services;

namespace Sketch.WebServer.Hubs
{
    public class SocialHub : Hub
    {
        private readonly IHubConnectionMapper<User> _connections;
        private readonly IHubSubscriptionMapper<string> _subscriptions;

        public SocialHub(
            IHubConnectionMapper<User> connections,
            IHubSubscriptionMapper<string> subscriptions)
        {
            _connections = connections;
            _subscriptions = subscriptions;
        }

        public async override Task OnConnectedAsync()
        {
            await _subscriptions.AddSubscriberAsync(Context.ConnectionId);
            await base.OnConnectedAsync();
        }

        public async override Task OnDisconnectedAsync(Exception exception)
        {
            await _subscriptions.RemoveSubscriberAsync(Context.ConnectionId);
            await _connections.RemoveAsync(Context.ConnectionId);
            await base.OnDisconnectedAsync(exception);
        }
    }
}
