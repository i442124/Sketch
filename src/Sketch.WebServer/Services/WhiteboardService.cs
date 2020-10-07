using System;
using System.Threading;
using System.Threading.Tasks;

using Sketch.Shared;
using Sketch.WebServer;
using Sketch.WebServer.Hubs;

namespace Sketch.WebServer.Services
{
    public class WhiteboardService : IWhiteboardService
    {
        private readonly INotificationService _notifyService;

        public WhiteboardService(INotificationService notifyService)
        {
            _notifyService = notifyService;
        }

        public Task DrawAsync(string channel, Stroke stroke)
        {
            return _notifyService.PublishAsync(channel, new StrokeEvent
            {
                Stroke = stroke, TimeStamp = DateTime.Now
            });
        }
    }
}
