using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Threading.Tasks;

using Sketch.Shared.Data;
using Sketch.Shared.Data.Invocations;

namespace Sketch.Shared.Services
{
    public class NotificationService : INotificationService
    {
        public Task InvokeAsync<T>(T content)
        {
            throw new NotImplementedException();
        }

        public IDisposable Subscribe<T>(Func<T, Task> handler)
        {
            throw new NotImplementedException();
        }
    }
}
