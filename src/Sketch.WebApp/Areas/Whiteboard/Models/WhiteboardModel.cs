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

        public Task SendAsync(Stroke stroke)
        {
            return _subscription.PublishAsync("/whiteboard", stroke);
        }

        public IDisposable OnReceive(Func<StrokeEvent, Task> handler)
        {
            return _subscription.OnReceive(handler);
        }
    }
}
