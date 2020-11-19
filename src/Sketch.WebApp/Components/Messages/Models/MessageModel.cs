using System;
using System.Threading;
using System.Threading.Tasks;

using Sketch.Shared;
using Sketch.WebApp.Components;

namespace Sketch.WebApp.Components
{
    public class MessageModel : IMessageModel
    {
        private readonly ISubscriptionModel _subscription;
        private readonly ISubscriptionEventModel<Message> _messageEvent;

        public MessageModel(
            ISubscriptionModel subscription,
            ISubscriptionEventModel<Message> messageEvent)
        {
            _subscription = subscription;
            _subscription.OnReceive<MessageEvent>(ReceiveAsync);
            _messageEvent = messageEvent;
        }

        public IDisposable OnReceive(Func<Message, Task> handler)
        {
            return _messageEvent.OnInvokeAsync(handler);
        }

        private async Task ReceiveAsync(MessageEvent messageEvent)
        {
            await _messageEvent.InvokeAsync(messageEvent.Message);
        }
    }
}
