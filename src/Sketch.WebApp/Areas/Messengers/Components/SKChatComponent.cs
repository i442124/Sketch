using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Components;

using Sketch.Shared;

namespace Sketch.WebApp.Areas.Messaging
{
    public class SKChatComponent : ComponentBase
    {
        private List<Message> _messages;

        public ReadOnlyCollection<Message> Messages
        {
            get { return _messages.AsReadOnly(); }
        }

        protected override void OnInitialized()
        {
            _messages = new List<Message>();
            Messenger.OnReceive(ReceiveAsync);
        }

        protected async Task SendAsync(Message message)
        {
            await Messenger.SendAsync(message);
        }

        protected async Task ReceiveAsync(MessageEvent e)
        {
            _messages.Add(e.Message);
            await InvokeAsync(StateHasChanged);
        }

        [Inject]
        private IMessengerModel Messenger { get; set; }
    }
}
