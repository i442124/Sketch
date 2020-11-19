using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sketch.WebApp.Components
{
    public class SubscriptionEventModel<T> : ISubscriptionEventModel<T>
    {
        private readonly InvocationHandlerList<T> _invocationList =
        new InvocationHandlerList<T>();

        public Task InvokeAsync(T contents)
        {
            return _invocationList.InvokeAsync(contents);
        }

        public IDisposable OnInvokeAsync(Func<T, Task> handler)
        {
            var invocationHandler = new InvocationHandler<T>(handler);
            return new Subscription<T>(invocationHandler, _invocationList);
        }
    }
}
