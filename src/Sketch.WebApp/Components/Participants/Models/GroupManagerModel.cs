using System;
using System.Threading;
using System.Threading.Tasks;

using Sketch.Shared;
using Sketch.WebApp.Components;

namespace Sketch.WebApp.Components
{
    public class GroupManagerModel : IGroupManagerModel
    {
        private readonly ISubscriptionModel _subscription;
        private readonly ISubscriptionEventModel<User> _userEvent;
        private readonly ISubscriptionEventModel<Subscribe> _subscribeEvent;
        private readonly ISubscriptionEventModel<Unsubscribe> _unsubscribeEvent;

        public GroupManagerModel(
            ISubscriptionModel subscription,
            ISubscriptionEventModel<User> userEvent,
            ISubscriptionEventModel<Subscribe> subscribeEvent,
            ISubscriptionEventModel<Unsubscribe> unsubscribeEvent)
        {
            _subscription = subscription;
            _subscription.OnReceive<UserEvent>(ReceiveAsync);
            _subscription.OnReceive<SubscribeEvent>(ReceiveAsync);
            _subscription.OnReceive<UnsubscribeEvent>(ReceiveAsync);

            _userEvent = userEvent;
            _subscribeEvent = subscribeEvent;
            _unsubscribeEvent = unsubscribeEvent;
        }

        public IDisposable OnReceive(Func<User, Task> handler)
        {
            return _userEvent.OnInvokeAsync(handler);
        }

        public IDisposable OnReceive(Func<Subscribe, Task> handler)
        {
            return _subscribeEvent.OnInvokeAsync(handler);
        }

        public IDisposable OnReceive(Func<Unsubscribe, Task> handler)
        {
            return _unsubscribeEvent.OnInvokeAsync(handler);
        }

        private async Task ReceiveAsync(UserEvent userEvent)
        {
            await _userEvent.InvokeAsync(userEvent.User);
        }

        private async Task ReceiveAsync(SubscribeEvent subscribeEvent)
        {
            await _subscribeEvent.InvokeAsync(subscribeEvent.Subscription);
        }

        private async Task ReceiveAsync(UnsubscribeEvent unsubscribeEvent)
        {
            await _unsubscribeEvent.InvokeAsync(unsubscribeEvent.Unsubscription);
        }
    }
}
