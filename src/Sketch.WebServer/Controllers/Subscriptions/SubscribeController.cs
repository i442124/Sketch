using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using Sketch.WebServer;
using Sketch.WebServer.Services;

namespace Sketch.WebServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubscribeController : Controller
    {
        private readonly IBroadcastService _broadcastService;

        public SubscribeController(IBroadcastService broadcastService)
        {
            _broadcastService = broadcastService;
        }

        [HttpGet("{subscriberId}/{channel}")]
        public async Task<IActionResult> Get(string subscriberId, string channel)
        {
            await _broadcastService.SubscribeAsync(subscriberId, channel);
            return Ok();
        }
    }
}
