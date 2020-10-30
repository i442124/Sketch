using System;
using System.Threading;
using System.Threading.Tasks;

using Sketch;
using Sketch.Shared;

namespace Sketch.WebApp.Areas.Whiteboard
{
    public interface IWhiteboardModel
    {
        Task SendAsync(Fill fill);

        Task SendAsync(Wipe wipe);

        Task SendAsync(Stroke stroke);

        IDisposable OnReceive(Func<FillEvent, Task> handler);

        IDisposable OnReceive(Func<WipeEvent, Task> handler);

        IDisposable OnReceive(Func<StrokeEvent, Task> handler);
    }
}
