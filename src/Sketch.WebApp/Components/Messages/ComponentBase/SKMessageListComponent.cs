using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Components;

using Sketch.Shared;
using Sketch.WebApp.Components;

namespace Sketch.WebApp.Components
{
    public class SKMessageListComponent : ComponentBase
    {
        private List<Message> _messages =
        new List<Message>(capacity: 128);

        public ReadOnlyCollection<Message> Messages
        {
            get { return _messages.AsReadOnly(); }
        }

        protected override void OnInitialized()
        {
            Message.OnReceive(ReceiveAsync);
        }

        protected async Task ReceiveAsync(Message message)
        {
            _messages.Add(message);
            await InvokeAsync(StateHasChanged);
        }

        [Inject]
        private IMessageModel Message { get; set; }
    }
}
