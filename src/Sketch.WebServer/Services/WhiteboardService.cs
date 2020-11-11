using System;
using System.Threading;
using System.Threading.Tasks;

using Sketch.Shared;
using Sketch.WebServer;
using Sketch.WebServer.Hubs;
using Sketch.WebServer.Storage;

namespace Sketch.WebServer.Services
{
    public class WhiteboardService : IWhiteboardService
    {
        private readonly INotificationService _notifyService;
        private readonly IWhiteboardStorage _whiteboardStorage;

        public WhiteboardService(INotificationService notifyService, IWhiteboardStorage whiteboardStorage)
        {
            _notifyService = notifyService;
            _whiteboardStorage = whiteboardStorage;
        }

        public async Task StrokeAsync(string channel, Stroke stroke)
        {
            var strokeEvent = new StrokeEvent { Stroke = stroke, TimeStamp = DateTime.Now };
            await _whiteboardStorage.PushAsync(channel, strokeEvent);
            await _notifyService.PublishAsync(channel, strokeEvent);
        }

        public async Task WipeAsync(string channel, Wipe wipe)
        {
            var wipeEvent = new WipeEvent { Wipe = wipe, Timestamp = DateTime.Now };
            await _notifyService.PublishAsync(channel, wipeEvent);
        }

        public async Task FillAsync(string channel, Fill fill)
        {
            var fillEvent = new FillEvent { Fill = fill, TimeStamp = DateTime.Now };
            await _notifyService.PublishAsync(channel, fillEvent);
        }

        public async Task ClearAsync(string channel, Clear clear)
        {
            var clearEvent = new ClearEvent { Clear = clear, TimeStamp = DateTime.Now };
            await _notifyService.PublishAsync(channel, clearEvent);
        }

        public async Task UndoAsync(string channel)
        {
            var clear = new Clear { Width = int.MaxValue, Height = int.MaxValue };
            var clearEvent = new ClearEvent { Clear = clear, TimeStamp = DateTime.Now };

            await _notifyService.PublishAsync(channel, clearEvent);

            foreach (var item in await _whiteboardStorage.PopAsync(channel))
            {
            }

            foreach (var item in _whiteboardStorage.GetStack(channel))
            {
                await _notifyService.PublishAsync(channel, item);
            }
        }
    }
}
