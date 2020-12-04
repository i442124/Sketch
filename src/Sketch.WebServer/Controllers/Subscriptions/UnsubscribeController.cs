using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using Sketch.WebServer;
using Sketch.WebServer.Services;

namespace Sketch.WebServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UnsubscribeController : Controller
    {
        private readonly IBroadcastService _broadcastService;

        public UnsubscribeController(IBroadcastService broadcastService)
        {
            _broadcastService = broadcastService;
        }

        [HttpGet("{subscriberId}/{channel}")]
        public async Task<IActionResult> Get(string subscriberId, string channel)
        {
            await _broadcastService.UnsubscribeAsync(subscriberId, channel);
            return Ok();
        }
    }
}
