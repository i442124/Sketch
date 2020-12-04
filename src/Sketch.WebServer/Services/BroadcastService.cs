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

        public Task WhisperAsync<T>(string subscriberId, T content)
        {
            return _context.Clients.Client(subscriberId).SendAsync($"{content.GetType()}", content);
        }

        public Task BroadcastAsync<T>(string subscriberId, T content)
        {
            return Task.WhenAll(_subscriptions.GetSubscriptions(subscriberId).Select(channel =>
            {
                return _context.Clients.GroupExcept(channel, subscriberId).SendAsync($"{content.GetType()}", content);
            }));
        }
    }
}
