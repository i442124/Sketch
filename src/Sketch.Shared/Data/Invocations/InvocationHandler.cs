using System;
using System.Threading;
using System.Threading.Tasks;

namespace Sketch.Shared.Data.Invocations
{
    public class InvocationHandler
    {
        private readonly Func<object[], Task> _callback;

        public InvocationHandler(Func<object[], Task> callback)
        {
            _callback = callback;
        }

        public async Task InvokeAsync(object[] parameters)
        {
            await _callback.Invoke(parameters);
        }
    }
}
