using System;
using System.Threading;
using System.Threading.Tasks;

namespace Sketch.WebApp.Components
{
    public interface ISubscriptionEventModel<T>
    {
        Task InvokeAsync(T contents);

        IDisposable OnInvokeAsync(Func<T, Task> handler);
    }
}
