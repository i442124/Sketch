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
        new HashSet<User>(capacity: 8);

        public ReadOnlyCollection<User> Members
        {
            get { return _members.ToList().AsReadOnly(); }
        }

        protected override void OnInitialized()
        {
            Group.OnReceive((Func<UserEvent, Task>)ReceiveAsync);
            Group.OnReceive((Func<SubscribeEvent, Task>)ReceiveAsync);
            Group.OnReceive((Func<UnsubscribeEvent, Task>)ReceiveAsync);
        }

        private async Task ReceiveAsync(UserEvent e)
        {
            if (!_members.Contains(e.User))
            {
                throw new ArgumentException("User is not subscribed to channel.");
            }

            _members.Remove(e.User);
            _members.Add(e.User);

            await InvokeAsync(StateHasChanged);
        }

        private async Task ReceiveAsync(SubscribeEvent e)
        {
            if (!_members.Add(e.User))
            {
                throw new ArgumentException("User is already subscribed to channel.");
            }

            await InvokeAsync(StateHasChanged);
        }

        private async Task ReceiveAsync(UnsubscribeEvent e)
        {
            if (!_members.Remove(e.User))
            {
                throw new ArgumentException("User is not subscribed to channel.");
            }

            await InvokeAsync(StateHasChanged);
        }

        [Inject]
        private IGroupManagerModel Group { get; set; }
    }
}
