using System.Threading;
using System.Threading.Tasks;

using Sketch.Shared;
using Sketch.WebServer;
using Sketch.WebServer.Services;

namespace Sketch.WebServer.Services
{
    public interface IWhiteboardService
    {
        Task StrokeAsync(string channel, string subscriberId, Stroke stroke);

        Task WipeAsync(string channel, string subscriberId, Wipe wipe);

        Task FillAsync(string channel, string subscriberId, Fill fill);

        Task ClearAsync(string channel, string subscriberId, Clear clear);

        Task UndoAsync(string channel, string subscriberId, Undo undo);
    }
}
