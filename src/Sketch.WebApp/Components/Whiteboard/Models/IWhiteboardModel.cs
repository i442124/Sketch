using System;
using System.Threading.Tasks;

using Sketch.Shared;
using Sketch.WebApp.Components;

namespace Sketch.WebApp.Components
{
    public interface IWhiteboardModel
    {
        public string ActionId { get; }

        Task StrokeAsync(Stroke stroke);

        Task WipeAsync(Wipe wipe);

        Task FillAsync(Fill fill);

        Task ClearAsync(Clear clear);

        Task UndoAsync(Undo undo);

        IDisposable OnReceive(Func<Stroke, Task> handler);

        IDisposable OnReceive(Func<Wipe, Task> handler);

        IDisposable OnReceive(Func<Fill, Task> handler);

        IDisposable OnReceive(Func<Clear, Task> handler);

        IDisposable OnReceive(Func<Undo, Task> handler);

        Task InvokeActionChanged();
    }
}
