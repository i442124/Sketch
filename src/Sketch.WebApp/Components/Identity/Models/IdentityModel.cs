using System.Threading;
using System.Threading.Tasks;

using Sketch.Shared;
using Sketch.WebApp.Components;

namespace Sketch.WebApp.Components
{
    public class IdentityModel : IIdentityModel
    {
        private readonly ISubscriptionModel _subscription;
        private readonly ISubscriptionEventModel<User> _userEvent;

        public User User { get; private set; }

        public IdentityModel(
            ISubscriptionModel subscription,
            ISubscriptionEventModel<User> userEvent)
        {
            _userEvent = userEvent;
            _subscription = subscription;
        }

        public async Task SetUserNameAsync(string name)
        {
            await SetUserIdentityAsync(new User { Name = name });
        }

        public async Task SetUserIdentityAsync(User identity)
        {
            await _subscription.RegisterAsync(identity);
            User = identity;
        }
    }
}
