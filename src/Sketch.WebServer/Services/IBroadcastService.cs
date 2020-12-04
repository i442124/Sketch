using System.Threading.Tasks;

namespace Sketch.WebServer.Services
{
    public interface IBroadcastService
    {
        Task WhisperAsync<T>(string subscriberId, T content);

        Task BroadcastAsync<T>(string subscriberId, T content);
    }
}
