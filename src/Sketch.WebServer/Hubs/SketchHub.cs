using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.SignalR;

using Sketch.WebServer;
using Sketch.WebServer.Services;

namespace Sketch.WebServer.Hubs
{
    public class SketchHub : Hub
    {
        private readonly IBroadcastService _service;

        public SketchHub(IBroadcastService service)
        {
            _service = service;
        }

        public async override Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
            await _service.RegisterAsync(Context.ConnectionId);
        }

        public async override Task OnDisconnectedAsync(Exception exception)
        {
            await base.OnDisconnectedAsync(exception);
            await _service.UnregisterAsync(Context.ConnectionId);
        }
    }
}
