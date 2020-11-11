using System;
using System.Threading;
using System.Threading.Tasks;

using Sketch.Shared;
using Sketch.WebApp.Components;

namespace Sketch.WebApp.Components
{
    public class MessageModel : IMessageModel
    {
        private ISubscriptionModel _subscription;

        public MessageModel(ISubscriptionModel subscription)
        {
            _subscription = subscription;
        }

        public IDisposable OnReceive(Func<MessageEvent, Task> handler)
        {
            return _subscription.OnReceive(handler);
        }
    }
}
