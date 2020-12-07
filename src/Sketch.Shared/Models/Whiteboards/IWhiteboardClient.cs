using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Sketch.Shared.Data;
using Sketch.Shared.Data.Ink;

namespace Sketch.Shared.Models
{
    public interface IWhiteboardClient
    {
        Task InvokeActionChanged();

        IEnumerable<Data.Action> Actions { get; }

        IDisposable OnStroke(Func<Stroke, Task> handler);

        IDisposable OnFill(Func<Fill, Task> handler);

        IDisposable OnWipe(Func<Wipe, Task> handler);

        IDisposable OnUndo(Func<Undo, Task> handler);

        IDisposable OnClear(Func<Clear, Task> handler);

        Task StrokeAsync(Stroke stroke);

        Task WipeAsync(Wipe wipe);

        Task FillAsync(Fill fill);

        Task UndoAsync(Undo undo);

        Task ClearAsync(Clear clear);
    }
}
