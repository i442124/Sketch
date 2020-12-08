using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Components;

using Sketch.Shared.Data;
using Sketch.Shared.Models;

namespace Sketch.WebApp.Components
{
    public abstract class SKMessagesComponent : ComponentBase
    {
        private readonly List<Message> _messages =
        new List<Message>(capacity: default);

        public ReadOnlyCollection<Message> Messages
        {
            get { return _messages.AsReadOnly(); }
        }

        protected override void OnInitialized()
        {
            Messenger.OnMessage(async message =>
            {
                _messages.Add(message);
                await InvokeAsync(StateHasChanged);
            });

            Messenger.OnMessageReceived(async message =>
            {
                _messages.Add(message);
                await InvokeAsync(StateHasChanged);
            });
        }

        [Inject]
        private IMessageClient Messenger { get; set; }
    }
}
