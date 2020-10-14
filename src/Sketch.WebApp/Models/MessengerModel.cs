using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

using Sketch.Shared;

namespace Sketch.WebApp.Models
{
    public class MessengerModel : IMessengerModel
    {
        private readonly HttpClient _http;
        private readonly ISubscriptionModel _subscription;

        public MessengerModel(HttpClient http, ISubscriptionModel subscription)
        {
            _http = http;

            _subscription = subscription;
        }

        public async Task SendAsync(Message message)
        {
            await _http.PostAsJsonAsync($"api/message/{_subscription.Channel}", message);
        }

        public IDisposable OnReceive(Func<MessageEvent, Task> handler)
        {
            return _subscription.OnReceive(handler);
        }
    }
}
