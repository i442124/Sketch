using System.Threading;
using System.Threading.Tasks;

using Sketch.Shared.Data.Ink;

namespace Sketch.WebServer.Services
{
    public interface IWhiteboardService
    {
        Task StrokeAsync(string subscriberId, Stroke stroke);

        Task WipeAsync(string subscriberId, Wipe wipe);

        Task FillAsync(string subscriberId, Fill fill);
    }
}
