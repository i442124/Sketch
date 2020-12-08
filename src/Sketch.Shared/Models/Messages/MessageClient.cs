using System;
using System.Threading;
using System.Threading.Tasks;

using Sketch.Shared.Data;
using Sketch.Shared.Services;

namespace Sketch.Shared.Models
{
    public class MessageClient
    {
        private readonly ISubscriptionService _subscriptions;
        private readonly INotificationService _notifications;

        public MessageClient(
            ISubscriptionService subscriptions,
            INotificationService notifications)
        {
            _subscriptions = subscriptions;
            _notifications = notifications;
        }

        public async Task SendAsync(Message message)
        {
            await _notifications.InvokeAsync(message);
            await _subscriptions.SendAsync("message", message);
        }

        public IDisposable OnMessage(Func<Message, Task> handler)
        {
            return _notifications.Subscribe(handler);
        }

        public IDisposable OnMessageReceive(Func<Message, Task> handler)
        {
            return _subscriptions.OnReceive(handler);
        }
    }
}
