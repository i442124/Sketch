using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sketch.Shared.Data.Invocations
{
    public class InvocationHandlerList
    {
        private readonly List<InvocationHandler> _invocationHandlers;

        public InvocationHandlerList()
        {
            _invocationHandlers = new List<InvocationHandler>();
        }

        public InvocationHandlerList(params InvocationHandler[] handlers)
        {
            _invocationHandlers = new List<InvocationHandler>(handlers);
        }

        public Task InvokeAsync(object[] parameters)
        {
            lock (_invocationHandlers)
            {
                return Task.WhenAll(_invocationHandlers.Select
                (
                    async task => await task.InvokeAsync(parameters))
                );
            }
        }

        public void Add(InvocationHandler handler)
        {
            lock (_invocationHandlers)
            {
                _invocationHandlers.Add(handler);
            }
        }

        public void Remove(InvocationHandler handler)
        {
            lock (_invocationHandlers)
            {
                _invocationHandlers.Remove(handler);
            }
        }
    }
}
