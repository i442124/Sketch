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
    }
}
