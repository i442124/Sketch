using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Components;

using Sketch.Shared.Data;
using Sketch.Shared.Models;

namespace Sketch.WebApp.Components
{
    public abstract class SKParticipantsComponent : ComponentBase
    {
        private readonly Dictionary<string, HashSet<User>> _members =
        new Dictionary<string, HashSet<User>>(capacity: default);

        public ReadOnlyDictionary<string, HashSet<User>> Members
        {
            get { return new ReadOnlyDictionary<string, HashSet<User>>(_members); }
        }

        protected override void OnInitialized()
        {
            Groups.OnUserChanged(async user =>
            {
                Console.WriteLine(user.Name);
                await UpdateUser(user);
            });

            Groups.OnUserSubscribed(async subscription =>
            {
                Console.WriteLine($"{subscription.Channel} - {subscription.User.Name}");
                await SubscribedAsync(subscription);
            });

            Groups.OnUserUnsubscribed(async subscription =>
            {
                Console.WriteLine($"{subscription.Channel} - {subscription.User.Name}");
                await UnsubscribeAsync(subscription);
            });
        }

        private async Task UpdateUser(User user)
        {
            var found = false;
            foreach (var (channel, members) in _members)
            {
                if (members.Contains(user))
                {
                    members.Remove(user);
                    members.Add(user);
                    found = true;
                }
            }

            if (!found)
            {
                throw new ArgumentNullException("User is not subscribed to any of the channels.");
            }

            await InvokeAsync(StateHasChanged);
        }

        private async Task SubscribedAsync(Subscribe subscription)
        {
            _members.TryAdd(subscription.Channel, new HashSet<User>());
            if (_members[subscription.Channel].Add(subscription.User))
            {
                await InvokeAsync(StateHasChanged);
                return;
            }

            throw new ArgumentException("User is already subscribed to channel.");
        }

        private async Task UnsubscribeAsync(Unsubscribe subscription)
        {
            _members.TryAdd(subscription.Channel, new HashSet<User>());
            if (_members[subscription.Channel].Remove(subscription.User))
            {
                if (_members[subscription.Channel].Count == 0)
                {
                    _members.Remove(subscription.Channel);
                }

                await InvokeAsync(StateHasChanged);
                return;
            }

            throw new ArgumentException("User is already subscribed to channel.");
        }

        [Inject]
        private IGroupClient Groups { get; set; }
    }
}
