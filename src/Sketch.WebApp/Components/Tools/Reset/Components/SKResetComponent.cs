﻿using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Components;

using Sketch.Shared;
using Sketch.WebApp.Components;

namespace Sketch.WebApp.Components
{
    public class SKResetComponent : ComponentBase
    {
        protected async Task ClearWhiteboardAsync()
        {
            await Whiteboard.SendAsync(Clear.All);
        }

        [Inject]
        private IWhiteboardModel Whiteboard { get; set; }
    }
}
