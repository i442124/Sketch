using System;
using System.Threading;
using System.Threading.Tasks;

using Sketch.Shared;
using Sketch.WebApp.Components;

namespace Sketch.WebApp.Components
{
    public class WhiteboardModel : IWhiteboardModel
    {
        private readonly ISubscriptionModel _subscription;

        public string ActionId { get; private set; }

        public WhiteboardModel(ISubscriptionModel subscription)
        {
            _subscription = subscription;
        }

        public Task SendAsync(Stroke stroke)
        {
            stroke.ActionId = ActionId;
            return _subscription.PublishAsync("whiteboard", "stroke", stroke);
        }

        public Task SendAsync(Wipe wipe)
        {
            wipe.ActionId = ActionId;
            return _subscription.PublishAsync("whiteboard", "wipe", wipe);
        }

        public Task SendAsync(Fill fill)
        {
            fill.ActionId = ActionId;
            return _subscription.PublishAsync("whiteboard", "fill", fill);
        }

        public Task SendAsync(Clear clear)
        {
            clear.ActionId = ActionId;
            return _subscription.PublishAsync("whiteboard", "clear", clear);
        }

        public Task SendAsync(Undo undo)
        {
            return _subscription.PublishAsync("whiteboard", "undo", undo);
        }

        public IDisposable OnReceive(Func<StrokeEvent, Task> handler)
        {
            return _subscription.OnReceive(handler);
        }

        public IDisposable OnReceive(Func<WipeEvent, Task> handler)
        {
            return _subscription.OnReceive(handler);
        }

        public IDisposable OnReceive(Func<FillEvent, Task> handler)
        {
            return _subscription.OnReceive(handler);
        }

        public IDisposable OnReceive(Func<ClearEvent, Task> handler)
        {
            return _subscription.OnReceive(handler);
        }

        public IDisposable OnReceive(Func<UndoEvent, Task> handler)
        {
            return _subscription.OnReceive(handler);
        }

        public Task InvokeActionChanged()
        {
            return Task.Run(() => ActionId = Guid.NewGuid().ToString());
        }
    }
}
