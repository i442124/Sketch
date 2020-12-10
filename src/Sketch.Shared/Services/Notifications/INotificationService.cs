using System;
using System.Threading;
using System.Threading.Tasks;

namespace Sketch.Shared.Services
{
    public interface INotificationService
    {
        Task InvokeAsync<T>(T content);

        IDisposable Subscribe<T>(Func<T, Task> handler);
    }
}
