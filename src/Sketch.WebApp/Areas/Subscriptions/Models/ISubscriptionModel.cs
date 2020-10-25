using System;
using System.Threading;
using System.Threading.Tasks;

namespace Sketch.WebApp.Areas.Subscriptions
{
    public interface ISubscriptionModel
    {
        string Channel { get; }

        string SubscriberId { get; }

        Task UnsubscribeAsync();

        Task SubscribeAsync(string channel);

        Task PublishAsync<T>(string route, T contents);

        IDisposable OnReceive<T>(Func<T, Task> handler);
    }
}
