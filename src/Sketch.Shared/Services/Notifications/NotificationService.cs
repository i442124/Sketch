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
        private readonly ConcurrentDictionary<Type, InvocationHandlerList> _handlers
        = new ConcurrentDictionary<Type, InvocationHandlerList>();

        public async Task InvokeAsync<T>(T content)
        {
            if (_handlers.TryGetValue(typeof(T), out InvocationHandlerList invocationHandlerList))
            {
                var parameters = new object[] { content };
                await invocationHandlerList.InvokeAsync(parameters);
            }
        }

        public IDisposable Subscribe<T>(Func<T, Task> handler)
        {
            var invocationHandler = new InvocationHandler(args => handler((T)args[0]));
            var invocationHandlerList = _handlers.AddOrUpdate(typeof(T), new InvocationHandlerList(invocationHandler), (_, invocations) =>
            {
                lock (invocations)
                {
                    invocations.Add(invocationHandler);
                }

                return invocations;
            });

            return new Subscription(invocationHandler, invocationHandlerList);
        }
    }
}
