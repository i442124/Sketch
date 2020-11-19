using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Components;

using Sketch.Shared;
using Sketch.WebApp.Components;

namespace Sketch.WebApp.Components
{
    public abstract class SKParticipantListComponent : ComponentBase
    {
        private readonly HashSet<User> _members =
        new HashSet<User>();

        public ReadOnlyCollection<User> Members
        {
            get { return _members.ToList().AsReadOnly(); }
        }

        protected override void OnInitialized()
        {
            GroupManager.OnReceive(UpdateAsync);
            GroupManager.OnReceive(SubscribedAsync);
            GroupManager.OnReceive(UnsubscribedAsync);
        }

        private async Task UpdateAsync(User user)
        {
            if (!_members.Contains(user))
            {
                throw new ArgumentException("User is not subscribed to this channel.");
            }

            _members.Remove(user);
            _members.Add(user);

            await InvokeAsync(StateHasChanged);
        }

        private async Task SubscribedAsync(Subscribe subscription)
        {
            if (!_members.Add(subscription.User))
            {
                throw new ArgumentException("User is already subscribed to channel.");
            }

            await InvokeAsync(StateHasChanged);
        }

        private async Task UnsubscribedAsync(Unsubscribe unsubscription)
        {
            if (!_members.Remove(unsubscription.User))
            {
                throw new ArgumentException("User is not subscribed to channel.");
            }

            await InvokeAsync(StateHasChanged);
        }

        [Inject]
        private IGroupManagerModel GroupManager { get; set; }
    }
}
