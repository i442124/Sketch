using System.Threading;
using System.Threading.Tasks;

using Sketch.Shared;
using Sketch.Shared.Data;

namespace Sketch.WebServer.Services
{
    public interface IMessengerService
    {
        Task SendAsync(string subscriberId, Message message);
    }
}
