using System.Threading;
using System.Threading.Tasks;

using Sketch.Shared;
using Sketch.WebApp.Components;

namespace Sketch.WebApp.Components
{
    public class IdentityModel : IIdentityModel
    {
        private readonly ISubscriptionModel _subscription;

        public string Name
        {
            get { return User.Name; }
        }

        public User User { get; private set; }

        public IdentityModel(ISubscriptionModel subscription)
        {
            _subscription = subscription;
        }

        public async Task SetUserIdentityAsync(User userIdentity)
        {
            await _subscription.RegisterAsync(userIdentity);
            User = userIdentity;
        }

        public async Task SetUserIdentityNameAsync(string userName)
        {
            await SetUserIdentityAsync(new User { Name = userName });
        }
    }
}
