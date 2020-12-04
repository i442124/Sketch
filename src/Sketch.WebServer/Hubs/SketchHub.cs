using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.SignalR;

namespace Sketch.WebServer.Hubs
{
    public class SketchHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            return base.OnDisconnectedAsync(exception);
        }
    }
}
