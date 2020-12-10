using System;
using System.Threading;
using System.Threading.Tasks;

using Sketch.Shared.Data;
using Sketch.Shared.Services;

namespace Sketch.Shared.Services
{
    public interface ISubscriptionService
    {
        string SubscriberId { get; }

        Task RegisterAsync(User user);

        Task SubscribeAsync(string channel);

        Task UnsubscribeAsync(string channel);

        IDisposable OnReceive<T>(Func<T, Task> handler);

        Task SendAsync<T>(string methodGroup, T content);

        Task SendAsync<T>(string methodGroup, string methodName, T content);
    }
}
