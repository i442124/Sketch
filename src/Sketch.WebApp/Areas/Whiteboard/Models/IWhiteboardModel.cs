using System;
using System.Threading;
using System.Threading.Tasks;

using Sketch;
using Sketch.Shared;

namespace Sketch.WebApp.Areas.Whiteboard
{
    public interface IWhiteboardModel
    {
        Task SendAsync(Stroke stroke);

        IDisposable OnReceive(Func<StrokeEvent, Task> handler);
    }
}
