using System;
using System.Threading;
using System.Threading.Tasks;

using Sketch.Shared;
using Sketch.WebApp.Areas;
using Sketch.WebApp.Areas.Subscriptions;

namespace Sketch.WebApp.Areas.Messaging
{
    public class MessengerModel : IMessengerModel
    {
        private readonly ISubscriptionModel _subscritpion;

        public MessengerModel(ISubscriptionModel subscription)
        {
            _subscritpion = subscription;
        }

        public Task SendAsync(Message message)
        {
            return _subscritpion.PublishAsync("/message", message);
        }

        public IDisposable OnReceive(Func<MessageEvent, Task> handler)
        {
            return _subscritpion.OnReceive(handler);
        }
    }
}
