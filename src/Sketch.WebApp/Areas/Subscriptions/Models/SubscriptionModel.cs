using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;

namespace Sketch.WebApp.Areas.Subscriptions
{
    public class SubscriptionModel : ISubscriptionModel
    {
        private readonly HttpClient _http;
        private readonly HubConnection _hubConnection;

        public string Channel
        {
            get; private set;
        }

        public string SubscriberId
        {
            get { return _hubConnection.ConnectionId; }
        }

        public SubscriptionModel(HttpClient http)
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl(new Uri(http.BaseAddress, "/hub"))
                .Build();

            _http = http;
        }

        public async Task UnsubscribeAsync()
        {
            if (_hubConnection.State != HubConnectionState.Connected)
            {
                await _hubConnection.StartAsync();
            }

            await _http.GetAsync($"/api/unsubscribe/{SubscriberId}/{Channel}");
        }

        public async Task SubscribeAsync(string channel)
        {
            if (_hubConnection.State != HubConnectionState.Connected)
            {
                await _hubConnection.StartAsync();
            }

            await _http.GetAsync($"/api/unsubscribe/{SubscriberId}/{Channel}");
            await _http.GetAsync($"/api/subscribe/{SubscriberId}/{Channel = channel}");
        }

        public async Task PublishAsync<T>(string route, T contents)
        {
            route = route.TrimStart('/');
            await _http.PostAsJsonAsync($"/api/{route}/{Channel}", contents);
        }

        public IDisposable OnReceive<T>(Func<T, Task> handler)
        {
            return _hubConnection.On($"ReceiveAsync_{typeof(T)}", handler);
        }
    }
}
