using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;

using Sketch.Shared.Data;
using Sketch.Shared.Services;

namespace Sketch.Shared.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly HttpClient _http;
        private readonly HubConnection _hubConnection;

        public SubscriptionService(HttpClient http)
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl(new Uri(http.BaseAddress, "/hub"))
                .Build();

            _http = http;
        }

        public string SubscriberId
        {
            get { return _hubConnection.ConnectionId; }
        }

        public async Task RegisterAsync(User user)
        {
            if (_hubConnection.State != HubConnectionState.Connected)
            {
                await _hubConnection.StartAsync();
            }

            await _http.PostAsJsonAsync($"/api/identity/{SubscriberId}", user);
        }

        public async Task SubscribeAsync(string channel)
        {
            if (_hubConnection.State != HubConnectionState.Connected)
            {
                await _hubConnection.StartAsync();
            }

            await _http.GetAsync($"/api/subscribe/{SubscriberId}/{channel}");
        }

        public async Task UnsubscribeAsync(string channel)
        {
            if (_hubConnection.State != HubConnectionState.Connected)
            {
                await _hubConnection.StartAsync();
            }

            await _http.GetAsync($"/api/unsubscribe/{SubscriberId}/{channel}");
        }

        public IDisposable OnReceive<T>(Func<T, Task> handler)
        {
            return _hubConnection.On($"{typeof(T)}", handler);
        }

        public Task SendAsync<T>(string methodGroup, T content)
        {
            return _http.PostAsJsonAsync($"/api/{methodGroup}/{SubscriberId}", content);
        }

        public Task SendAsync<T>(string methodGroup, string methodName, T content)
        {
            return _http.PostAsJsonAsync($"/api/{methodGroup}/{methodName}/{SubscriberId}", content);
        }
    }
}
