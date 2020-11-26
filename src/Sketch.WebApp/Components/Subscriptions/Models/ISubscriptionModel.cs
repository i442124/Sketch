using System;
using System.Threading;
using System.Threading.Tasks;

using Sketch.Shared;
using Sketch.WebApp.Components;

namespace Sketch.WebApp.Components
{
    public interface ISubscriptionModel
    {
        string Channel { get; }

        string SubscriberId { get; }

        Task RegisterAsync(User user);

        Task SubscribeAsync(string channel);

        Task UnsubscribeAsync();

        IDisposable OnReceive<T>(Func<T, Task> handler);

        Task SendAsync<T>(string methodGroup, T content);

        Task SendAsync<T>(string methodGroup, string methodName, T content);
    }
}
