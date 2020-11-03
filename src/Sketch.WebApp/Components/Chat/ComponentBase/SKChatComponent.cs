using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Components;

using Sketch.Shared;
using Sketch.WebApp.Components;

namespace Sketch.WebApp.Components
{
    public abstract class SKChatComponent : ComponentBase
    {
        public string Contents { get; set; }

        protected async Task SendAsync()
        {
            await SendAsync(clearAfterSend: true);
        }

        protected async Task SendAsync(bool clearAfterSend)
        {
            await Messenger.SendAsync(new Message
            {
                User = Identity.User, Contents = Contents,
            });

            if (clearAfterSend)
            {
                Contents = string.Empty;
                await InvokeAsync(StateHasChanged);
            }
        }

        [Inject]
        private IMessengerModel Messenger { get; set; }

        [Inject]
        private IIdentityModel Identity { get; set; }
    }
}
