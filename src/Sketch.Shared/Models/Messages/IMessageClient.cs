using System;
using System.Threading;
using System.Threading.Tasks;

using Sketch.Shared.Data;

namespace Sketch.Shared.Models
{
    public interface IMessageClient
    {
        Task SendAsync(Message message);

        IDisposable OnMessage(Func<Message, Task> handler);

        IDisposable OnMessageReceived(Func<Message, Task> handler);
    }
}
