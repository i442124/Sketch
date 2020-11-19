using System;
using System.Threading;
using System.Threading.Tasks;

using Sketch.Shared;
using Sketch.WebApp.Components;

namespace Sketch.WebApp.Components
{
    public class MessengerModel : IMessengerModel
    {
        private readonly ISubscriptionModel _subscription;
        private readonly ISubscriptionEventModel<Message> _messageEvent;

        public MessengerModel(
            ISubscriptionModel subscription,
            ISubscriptionEventModel<Message> messageEvent)
        {
            _subscription = subscription;
            _messageEvent = messageEvent;
        }

        public async Task SendAsync(Message message)
        {
            await _messageEvent.InvokeAsync(message);
            await _subscription.SendAsync("message", message);
        }
    }
}
