using System.Collections;
using System.Threading.Tasks;

using Sketch.Shared;

namespace Sketch.WebServer.Storage
{
    public interface IWhiteboardStorage
    {
        IEnumerable Pop(string channel);

        Task<IEnumerable> PopAsync(string channel);

        void Push(string channel, StrokeEvent stroke);

        Task PushAsync(string channel, StrokeEvent stroke);

        IEnumerable GetStack(string channel);
    }
}
