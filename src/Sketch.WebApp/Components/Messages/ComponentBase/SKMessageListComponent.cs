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

        protected async Task ReceiveAsync(MessageEvent e)
        {
            System.Console.WriteLine("Test!");
            System.Console.WriteLine(e.Message.Contents);

            _messages.Add(e.Message);
            await InvokeAsync(StateHasChanged);
        }

        [Inject]
        private IMessageModel Message { get; set; }
    }
}
