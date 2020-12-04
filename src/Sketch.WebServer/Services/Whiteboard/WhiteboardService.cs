using System;
using System.Threading;
using System.Threading.Tasks;

using Sketch.Shared.Data;
using Sketch.Shared.Data.Ink;

namespace Sketch.WebServer.Services
{
    public class WhiteboardService : IWhiteboardService
    {
        private readonly IBroadcastService _broadcastService;

        public WhiteboardService(IBroadcastService broadcastService)
        {
            _broadcastService = broadcastService;
        }

        public Task StrokeAsync(string subscriberId, Stroke stroke)
        {
            return _broadcastService.BroadcastAsync(subscriberId, stroke);
        }

        public Task WipeAsync(string subscriberId, Wipe wipe)
        {
            return _broadcastService.BroadcastAsync(subscriberId, wipe);
        }

        public Task FillAsync(string subscriberId, Fill fill)
        {
            return _broadcastService.BroadcastAsync(subscriberId, fill);
        }
    }
}
