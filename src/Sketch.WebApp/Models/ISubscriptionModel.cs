using System;
using System.Threading.Tasks;

namespace Sketch.WebApp.Models
{
    public interface ISubscriptionModel
    {
        string Channel { get; }

        Task UnsubscribeAsync();

        Task SubscribeAsync(string channel);

        IDisposable OnReceive<T>(Func<T, Task> handler);
    }
}
