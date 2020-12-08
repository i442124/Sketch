using System;
using System.Threading;
using System.Threading.Tasks;

using Sketch.Shared.Data;
using Sketch.Shared.Services;

namespace Sketch.Shared.Models
{
    public class GroupClient
    {
        private readonly ISubscriptionService _subscriptions;

        public GroupClient(ISubscriptionService subscriptions)
        {
            _subscriptions = subscriptions;
        }

        public IDisposable OnUserChanged(Func<User, Task> handler)
        {
            return _subscriptions.OnReceive(handler);
        }

        public IDisposable OnUserSubscribed(Func<Subscribe, Task> handler)
        {
            return _subscriptions.OnReceive(handler);
        }

        public IDisposable OnuserUnsubscribed(Func<Unsubscribe, Task> handler)
        {
            return _subscriptions.OnReceive(handler);
        }
    }
}
