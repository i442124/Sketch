using System;
using System.Threading;
using System.Threading.Tasks;

using Sketch.Shared.Data;
using Sketch.Shared.Services;

namespace Sketch.Shared.Models
{
    public class UserIdentity : IUserIdentity
    {
        private readonly ISubscriptionService _subscriptions;
        private readonly INotificationService _notifications;

        public User User { get; private set; }

        public UserIdentity(
            ISubscriptionService subscriptions,
            INotificationService notifications)
        {
            _subscriptions = subscriptions;
            _notifications = notifications;
        }

        public async Task SetUsernameAsync(string name)
        {
            var user = new User { Name = name, Guid = Guid.NewGuid() };
            await _subscriptions.RegisterAsync(user);
            User = user;
        }
    }
}
