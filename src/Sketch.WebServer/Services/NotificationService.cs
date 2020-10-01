using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.SignalR;

using Sketch.Shared;
using Sketch.WebServer;
using Sketch.WebServer.Hubs;

namespace Sketch.WebServer.Services
{
    public class NotificationService : INotificationService
    {
        private const string CALLBACK_METHOD = "ReceiveAsync";
        private readonly IHubContext<SocialHub> _context;

        public NotificationService(
            IHubContext<SocialHub> context)
        {
            _context = context;
        }

        public Task PublishAsync<T>(string channel, T content)
        {
            return _context.Clients.Group(channel).SendAsync(CALLBACK_METHOD, content);
        }

        public Task SubscribeAsync(string subscriberId, string channel)
        {
            return _context.Groups.AddToGroupAsync(subscriberId, channel);
        }

        public Task UnsubscribeAsync(string subscriberId, string channel)
        {
            return _context.Groups.RemoveFromGroupAsync(subscriberId, channel);
        }
    }
}
