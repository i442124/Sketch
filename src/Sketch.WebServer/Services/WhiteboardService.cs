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

        public Task StrokeAsync(string channel, string subscriberId, Stroke stroke)
        {
            return _notifyService.BroadcastAsync(channel, subscriberId, new StrokeEvent
            {
                Stroke = stroke, TimeStamp = DateTime.Now
            });
        }

        public Task WipeAsync(string channel, string subscriberId, Wipe wipe)
        {
            return _notifyService.BroadcastAsync(channel, subscriberId, new WipeEvent
            {
                Wipe = wipe, Timestamp = DateTime.Now
            });
        }

        public Task FillAsync(string channel, string subscriberId, Fill fill)
        {
            return _notifyService.BroadcastAsync(channel, subscriberId, new FillEvent
            {
                Fill = fill, TimeStamp = DateTime.Now
            });
        }

        public Task ClearAsync(string channel, string subscriberId, Clear clear)
        {
            return _notifyService.BroadcastAsync(channel, subscriberId, new ClearEvent
            {
                Clear = clear, TimeStamp = DateTime.Now
            });
        }

        public Task UndoAsync(string channel, string subscriberId, Undo undo)
        {
            return _notifyService.BroadcastAsync(channel, subscriberId, new UndoEvent
            {
                Undo = undo, TimeStamp = DateTime.Now
            });
        }
    }
}
