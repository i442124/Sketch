using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Components;

namespace Sketch.WebApp.Areas.Subscriptions
{
    public abstract class SKChannelComponent : ComponentBase
    {
        protected async Task UnsubscribeAsync()
        {
            await Subscription.UnsubscribeAsync();
        }

        protected async Task SubscribeAsync(string channel)
        {
            await Subscription.SubscribeAsync(channel);
        }

        [Inject]
        private ISubscriptionModel Subscription { get; set; }
    }
}
