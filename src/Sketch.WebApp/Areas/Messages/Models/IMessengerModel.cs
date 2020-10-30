using System;
using System.Threading;
using System.Threading.Tasks;

using Sketch;
using Sketch.Shared;

namespace Sketch.WebApp.Areas.Messages
{
    public interface IMessengerModel
    {
        Task SendAsync(Message message);

        IDisposable OnReceive(Func<MessageEvent, Task> handler);
    }
}
