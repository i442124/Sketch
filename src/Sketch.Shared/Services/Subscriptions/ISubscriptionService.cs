using System;
using System.Threading;
using System.Threading.Tasks;

namespace Sketch.Shared.Services
{
    public interface ISubscriptionService
    {
        string SubscriberId { get; }

        Task SubscribeAsync(string channel);

        Task UnsubscribeAsync(string channel);

        IDisposable OnReceive<T>(Func<T, Task> handler);

        Task SendAsync<T>(string methodGroup, T content);

        Task SendAsync<T>(string methodGroup, string methodName, T content);
    }
}
