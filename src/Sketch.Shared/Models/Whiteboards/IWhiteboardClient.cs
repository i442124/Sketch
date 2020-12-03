using System;
using System.Threading;
using System.Threading.Tasks;

using Sketch.Shared.Data;
using Sketch.Shared.Data.Ink;

namespace Sketch.Shared.Models
{
    public interface IWhiteboardClient
    {
        Task InvokeActionChanged();

        IDisposable OnStroke(Func<Stroke, Task> handler);

        IDisposable OnFill(Func<Fill, Task> handler);

        IDisposable OnWipe(Func<Wipe, Task> handler);

        Task StrokeAsync(Stroke stroke);

        Task WipeAsync(Wipe wipe);

        Task FillAsync(Fill fill);
    }
}
