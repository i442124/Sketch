using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Components;

using Sketch.Shared.Data;
using Sketch.Shared.Models;

namespace Sketch.WebApp.Components
{
    public abstract class SKChatComponent : ComponentBase
    {
        protected Task SendAsync(string contents)
        {
            return Messenger.SendAsync(new Message
            {
                User = Identity.User, Contents = contents
            });
        }

        [Inject]
        private IUserIdentity Identity { get; set; }

        [Inject]
        private IMessageClient Messenger { get; set; }
    }
}
