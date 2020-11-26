using System;
using System.Threading;
using System.Threading.Tasks;

namespace Sketch.WebApp.Components
{
    public class Subscription<T> : IDisposable
    {
        private readonly InvocationHandler<T> _invocationHandler;
        private readonly InvocationHandlerList<T> _invocationHandlerList;

        public Subscription(InvocationHandler<T> handler, InvocationHandlerList<T> handlerList)
        {
            _invocationHandler = handler;
            _invocationHandlerList = handlerList;
            _invocationHandlerList.Add(handler: handler);
        }

        public void Dispose()
        {
            _invocationHandlerList.Remove(_invocationHandler);
        }
    }
}
