using System;
using System.Threading;
using System.Threading.Tasks;

using Sketch.Shared;
using Sketch.WebApp.Areas;
using Sketch.WebApp.Areas.Subscriptions;

namespace Sketch.WebApp.Areas.Whiteboard
{
    public class WhiteboardModel : IWhiteboardModel
    {
        private readonly ISubscriptionModel _subscription;

        public WhiteboardModel(ISubscriptionModel subscription)
        {
            _subscription = subscription;
        }

        public Task SendAsync(Fill fill)
        {
            return _subscription.PublishAsync("/whiteboard/fill", fill);
        }

        public Task SendAsync(Wipe wipe)
        {
            return _subscription.PublishAsync("/whiteboard/wipe", wipe);
        }

        public Task SendAsync(Clear clear)
        {
            return _subscription.PublishAsync("/whiteboard/clear", clear);
        }

        public Task SendAsync(Stroke stroke)
        {
            return _subscription.PublishAsync("/whiteboard/stroke", stroke);
        }

        public IDisposable OnReceive(Func<FillEvent, Task> handler)
        {
            return _subscription.OnReceive(handler);
        }

        public IDisposable OnReceive(Func<WipeEvent, Task> handler)
        {
            return _subscription.OnReceive(handler);
        }

        public IDisposable OnReceive(Func<ClearEvent, Task> handler)
        {
            return _subscription.OnReceive(handler);
        }

        public IDisposable OnReceive(Func<StrokeEvent, Task> handler)
        {
            return _subscription.OnReceive(handler);
        }
    }
}
