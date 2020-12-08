using System;
using System.Threading;
using System.Threading.Tasks;

using Sketch.Shared.Data;

namespace Sketch.Shared.Models
{
    public interface IGroupClient
    {
        IDisposable OnUserChanged(Func<User, Task> handler);

        IDisposable OnUserSubscribed(Func<Subscribe, Task> handler);

        IDisposable OnUserUnsubscribed(Func<Unsubscribe, Task> handler);
    }
}
