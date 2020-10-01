using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

using Sketch.Shared;

namespace Sketch.WebApp.Models
{
    public class WhiteboardModel : IWhiteboardModel
    {
        private readonly HttpClient _http;
        private readonly ISubscriptionModel _subscription;

        public WhiteboardModel(HttpClient http, ISubscriptionModel subscription)
        {
            _http = http;

            _subscription = subscription;
        }

        public async Task SendAsync(Stroke stroke)
        {
            await _http.PostAsJsonAsync($"api/whiteboard/{_subscription.Channel}", stroke);
        }

        public IDisposable OnReceive(Func<StrokeEvent, Task> handler)
        {
            return _subscription.OnReceive(handler);
        }
    }
}
