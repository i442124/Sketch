using System.Threading;
using System.Threading.Tasks;

using Sketch.Shared;
using Sketch.WebServer;
using Sketch.WebServer.Hubs;

namespace Sketch.WebServer.Services
{
    public class MessengerService : IMessengerService
    {
        private readonly INotificationService _notifyService;

        public MessengerService(
            INotificationService notifyService)
        {
            _notifyService = notifyService;
        }

        public Task SendAsync(string channel, string message)
        {
            return _notifyService.PublishAsync(channel, message);
        }
    }
}
