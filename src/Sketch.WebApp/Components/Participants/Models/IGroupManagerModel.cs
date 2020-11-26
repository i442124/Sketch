using System;
using System.Threading;
using System.Threading.Tasks;

using Sketch.Shared;
using Sketch.WebApp.Components;

namespace Sketch.WebApp.Components
{
    public interface IGroupManagerModel
    {
        IDisposable OnReceive(Func<User, Task> handler);

        IDisposable OnReceive(Func<Subscribe, Task> handler);

        IDisposable OnReceive(Func<Unsubscribe, Task> handler);
    }
}
