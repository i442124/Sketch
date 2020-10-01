using System;
using System.Threading;
using System.Threading.Tasks;

using Sketch;
using Sketch.Shared;

namespace Sketch.WebApp.Models
{
    public interface IWhiteboardModel
    {
        public Task SendAsync(Stroke stroke);

        public IDisposable OnReceive(Func<StrokeEvent, Task> handler);
    }
}
