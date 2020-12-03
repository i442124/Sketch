using System;
using System.Threading;
using System.Threading.Tasks;

namespace Sketch.Shared.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        public string SubscriberId
        {
            get { throw new NotImplementedException(); }
        }

        public Task Subscribe(string channel)
        {
            throw new NotImplementedException();
        }

        public Task Unsubscribe(string channel)
        {
            throw new NotImplementedException();
        }

        public IDisposable OnReceive<T>(Func<T, Task> handler)
        {
            throw new NotImplementedException();
        }

        public Task SendAsync<T>(string methodGroup, T content)
        {
            throw new NotImplementedException();
        }

        public Task SendAsync<T>(string methodGroup, string methodName, T content)
        {
            throw new NotImplementedException();
        }
    }
}
