using System.Threading;
using System.Threading.Tasks;

using Sketch.Shared;
using Sketch.WebServer;
using Sketch.WebServer.Services;

namespace Sketch.WebServer.Services
{
    public interface IWhiteboardService
    {
        Task StrokeAsync(string channel, Stroke stroke);

        Task WipeAsync(string channel, Wipe wipe);

        Task FillAsync(string channel, Fill fill);

        Task ClearAsync(string channel, Clear clear);

        Task UndoAsync(string channel, Undo undo);
    }
}
