using System;
using System.Threading;
using System.Threading.Tasks;

using Sketch;
using Sketch.Shared;

namespace Sketch.WebApp.Components
{
    public interface IWhiteboardModel
    {
        string ActionId { get; }

        Task SendAsync(Stroke stroke);

        Task SendAsync(Wipe wipe);

        Task SendAsync(Fill fill);

        Task SendAsync(Clear clear);

        Task SendAsync(Undo undo);

        IDisposable OnReceive(Func<StrokeEvent, Task> handler);

        IDisposable OnReceive(Func<WipeEvent, Task> handler);

        IDisposable OnReceive(Func<FillEvent, Task> handler);

        IDisposable OnReceive(Func<ClearEvent, Task> handler);

        IDisposable OnReceive(Func<UndoEvent, Task> handler);

        Task InvokeActionChanged();
    }
}
