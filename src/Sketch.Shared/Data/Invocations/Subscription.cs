using System;

namespace Sketch.Shared.Data.Invocations
{
    public class Subscription : IDisposable
    {
        private readonly InvocationHandler _invocationHandler;
        private readonly InvocationHandlerList _invocationHandlerList;

        public Subscription(InvocationHandler invocationHandler, InvocationHandlerList invocationHandlerList)
        {
            _invocationHandler = invocationHandler;
            _invocationHandlerList = invocationHandlerList;
        }

        public void Dispose()
        {
            _invocationHandlerList.Remove(_invocationHandler);
        }
    }
}
