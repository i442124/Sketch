using System;
using System.Threading;
using System.Threading.Tasks;

namespace Sketch.WebApp.Components
{
    public struct InvocationHandler<T>
    {
        private readonly Func<T, Task> _callback;

        public InvocationHandler(Func<T, Task> callback)
        {
            _callback = callback;
        }

        public async Task InvokeAsync(T parameter)
        {
            await _callback.Invoke(parameter);
        }
    }
}
