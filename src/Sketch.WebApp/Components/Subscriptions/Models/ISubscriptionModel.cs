using System;
using System.Threading;
using System.Threading.Tasks;

using Sketch;
using Sketch.Shared;

namespace Sketch.WebApp.Components
{
    public interface ISubscriptionModel
    {
        string Channel { get; }

        string SubscriberId { get; }

        Task UnsubscribeAsync();

        Task RegisterAsync(User user);

        Task SubscribeAsync(string channel);

        Task PublishAsync<T>(string methodGroup, string methodName, T contents);

        IDisposable OnReceive<T>(Func<T, Task> handler);
    }
}
