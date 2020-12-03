using System;
using System.Threading.Tasks;

using Sketch.Shared.Data;
using Sketch.Shared.Data.Ink;
using Sketch.Shared.Services;

namespace Sketch.Shared.Models
{
    public class WhiteboardClient : IWhiteboardClient
    {
        private readonly ISubscriptionService _subscriptions;
        private readonly INotificationService _notifications;

        public WhiteboardClient(
            ISubscriptionService subscriptions,
            INotificationService notifications)
        {
            subscriptions.OnReceive<Stroke>(notifications.InvokeAsync);
            subscriptions.OnReceive<Wipe>(notifications.InvokeAsync);
            subscriptions.OnReceive<Fill>(notifications.InvokeAsync);

            _subscriptions = subscriptions;
            _notifications = notifications;
        }

        public IDisposable OnStroke(Func<Stroke, Task> handler)
        {
            return _notifications.Subscribe(handler);
        }

        public IDisposable OnFill(Func<Fill, Task> handler)
        {
            return _notifications.Subscribe(handler);
        }

        public IDisposable OnWipe(Func<Wipe, Task> handler)
        {
            return _notifications.Subscribe(handler);
        }

        public async Task StrokeAsync(Stroke stroke)
        {
            await _notifications.InvokeAsync(stroke);
            await _subscriptions.SendAsync("whiteboard", "stroke", stroke);
        }

        public async Task FillAsync(Fill fill)
        {
            await _notifications.InvokeAsync(fill);
            await _subscriptions.SendAsync("whiteboard", "fill", fill);
        }

        public async Task WipeAsync(Wipe wipe)
        {
            await _notifications.InvokeAsync(wipe);
            await _subscriptions.SendAsync("whiteboard", "wipe", wipe);
        }
    }
}
