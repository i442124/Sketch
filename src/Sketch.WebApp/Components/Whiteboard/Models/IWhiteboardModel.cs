using System;
using System.Threading;
using System.Threading.Tasks;

using Sketch;
using Sketch.Shared;

namespace Sketch.WebApp.Components
{
    public interface IWhiteboardModel
    {
        Task SendAsync(Fill fill);

        Task SendAsync(Wipe wipe);

        Task SendAsync(Clear clear);

        Task SendAsync(Stroke stroke);

        IDisposable OnReceive(Func<FillEvent, Task> handler);

        IDisposable OnReceive(Func<WipeEvent, Task> handler);

        IDisposable OnReceive(Func<ClearEvent, Task> handler);

        IDisposable OnReceive(Func<StrokeEvent, Task> handler);
    }
}
