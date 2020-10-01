using System.Threading.Tasks;

namespace Sketch.WebServer.Services
{
    public interface IMessengerService
    {
        Task SendAsync(string channel, string message);
    }
}
