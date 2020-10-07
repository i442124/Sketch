using System.Threading;
using System.Threading.Tasks;

using Sketch.Shared;
using Sketch.WebServer;
using Sketch.WebServer.Services;

namespace Sketch.WebServer.Services
{
    public interface IMessengerService
    {
        Task SendAsync(string channel, Message message);
    }
}
