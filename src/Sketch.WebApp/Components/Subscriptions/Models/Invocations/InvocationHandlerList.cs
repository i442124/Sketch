using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sketch.WebApp.Components
{
    public class InvocationHandlerList<T>
    {
        private List<InvocationHandler<T>> _invocationHandlers =
        new List<InvocationHandler<T>>();

        public InvocationHandlerList()
        {
        }

        public void Add(InvocationHandler<T> handler)
        {
            lock (_invocationHandlers)
            {
                _invocationHandlers.Add(handler);
            }
        }

        public void Remove(InvocationHandler<T> handler)
        {
            lock (_invocationHandlers)
            {
                _invocationHandlers.Remove(handler);
            }
        }

        public Task InvokeAsync(T parameter)
        {
            lock (_invocationHandlers)
            {
                return Task.WhenAll(_invocationHandlers.Select
                (
                    async task => await task.InvokeAsync(parameter))
                );
            }
        }
    }
}
