using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Components;

using Sketch.Shared;
using Sketch.Shared.Services;

namespace Sketch.WebApp.Components
{
    public abstract class SKChannelComponent : ComponentBase
    {
        protected Task SubscribeAsync(string channel)
        {
            return Subscription.SubscribeAsync(channel);
        }

        protected Task UnsubscribeAsync(string channel)
        {
            return Subscription.UnsubscribeAsync(channel);
        }

        [Inject]
        private ISubscriptionService Subscription { get; set; }
    }
}
