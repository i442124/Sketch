using System.Threading;
using System.Threading.Tasks;

using Sketch.Shared;
using Sketch.Shared.Data;

namespace Sketch.WebServer.Services
{
    public class MessengerService
    {
        private readonly IBroadcastService _broadcastService;

        public MessengerService(IBroadcastService broadcastService)
        {
            _broadcastService = broadcastService;
        }

        public Task SendAsync(string subscriberId, Message message)
        {
            return _broadcastService.BroadcastAsync(subscriberId, message);
        }
    }
}
