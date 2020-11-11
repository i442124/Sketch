using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Components;

namespace Sketch.WebApp.Components
{
    public abstract class SKChannelComponent : ComponentBase
    {
        public string Channel
        {
            get { return Subscription.Channel; }
        }

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
