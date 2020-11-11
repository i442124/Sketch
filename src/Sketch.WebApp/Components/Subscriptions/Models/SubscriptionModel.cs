using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;

using Sketch;
using Sketch.Shared;

namespace Sketch.WebApp.Components
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

        public async Task RegisterAsync(User user)
        {
            if (_hubConnection.State != HubConnectionState.Connected)
            {
                await _hubConnection.StartAsync();
            }

            await _http.GetAsync($"/api/register/{SubscriberId}/{user.Name}");
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

        public async Task PublishAsync<T>(string methodGroup, string methodName, T contents)
        {
            await _http.PostAsJsonAsync($"/api/{methodGroup}/{Channel}/{methodName}", contents);
        }

        public IDisposable OnReceive<T>(Func<T, Task> handler)
        {
            return _hubConnection.On($"{typeof(T)}", handler);
        }
    }
}
