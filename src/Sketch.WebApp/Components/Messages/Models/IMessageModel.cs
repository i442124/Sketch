using System;
using System.Threading;
using System.Threading.Tasks;

using Sketch.Shared;
using Sketch.WebApp.Components;

namespace Sketch.WebApp.Components
{
    public interface IMessageModel
    {
        IDisposable OnReceive(Func<Message, Task> handler);
    }
}
