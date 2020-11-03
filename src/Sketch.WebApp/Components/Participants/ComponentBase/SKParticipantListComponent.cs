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
            Group.OnReceive(ReceiveAsync);
        }

        private async Task ReceiveAsync(UserEvent e)
        {
            _members.Remove(e.User);
            if (e.Connected)
            {
                _members.Add(e.User);
            }

            await InvokeAsync(StateHasChanged);
        }

        [Inject]
        private IGroupManagerModel Group { get; set; }
    }
}
