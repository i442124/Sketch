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

        public GroupManagerModel(ISubscriptionModel subscription)
        {
            _subscription = subscription;
        }

        public IDisposable OnReceive(Func<UserEvent, Task> handler)
        {
            return _subscription.OnReceive(handler);
        }
    }
}
