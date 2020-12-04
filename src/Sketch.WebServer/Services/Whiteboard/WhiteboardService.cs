using System;
using System.Threading;
using System.Threading.Tasks;

using Sketch.Shared.Data;
using Sketch.Shared.Data.Ink;

namespace Sketch.WebServer.Services
{
    public class WhiteboardService : IWhiteboardService
    {
        private readonly IBroadcastService _broadcaster;

        public WhiteboardService(IBroadcastService broadcaster)
        {
            _broadcaster = broadcaster;
        }

        public Task StrokeAsync(string subscriberId, Stroke stroke)
        {
            return _broadcaster.BroadcastAsync(subscriberId, stroke);
        }

        public Task WipeAsync(string subscriberId, Wipe wipe)
        {
            return _broadcaster.BroadcastAsync(subscriberId, wipe);
        }

        public Task FillAsync(string subscriberId, Fill fill)
        {
            return _broadcaster.BroadcastAsync(subscriberId, fill);
        }
    }
}
