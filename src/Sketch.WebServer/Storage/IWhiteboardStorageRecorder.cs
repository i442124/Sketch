using System;
using System.Collections;
using System.Threading.Tasks;

using Sketch;
using Sketch.Shared;

namespace Sketch.WebServer.Storage
{
    public interface IWhiteboardStorageRecorder
    {
        IEnumerable Pop();

        Task<IEnumerable> PopAsync();

        void Push(StrokeEvent stroke);

        Task PushAsync(StrokeEvent stroke);

        IEnumerable GetStack();

        Task<IEnumerable> GetStackAsync();
    }
}
