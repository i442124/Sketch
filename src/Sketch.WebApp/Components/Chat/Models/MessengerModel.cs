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

        public MessengerModel(ISubscriptionModel subscription)
        {
            _subscription = subscription;
        }

        public async Task SendAsync(Message message)
        {
            await _subscription.PublishAsync("message", null, message);
        }
    }
}
