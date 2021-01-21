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
        private readonly List<(bool SendBySelf, Message Message)> _posts =
        new List<(bool SendBySelf, Message Message)>(capacity: default);

        public ReadOnlyCollection<(bool SendBySelf, Message Message)> Posts
        {
            get { return _posts.AsReadOnly(); }
        }

        protected override void OnInitialized()
        {
            Messenger.OnMessage(async message =>
            {
                _posts.Add((true, message));
                await InvokeAsync(StateHasChanged);
            });

            Messenger.OnMessageReceived(async message =>
            {
                _posts.Add((false, message));
                await InvokeAsync(StateHasChanged);
            });
        }

        [Inject]
        private IMessageClient Messenger { get; set; }
    }
}
