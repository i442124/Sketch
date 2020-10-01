using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Sketch.WebServer;
using Sketch.WebServer.Services;

namespace Sketch.WebServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscribeController : Controller
    {
        private readonly INotificationService _notifyService;

        public SubscribeController(INotificationService notifyService)
        {
            _notifyService = notifyService;
        }

        [HttpGet("{subscriberId}/{channel}")]
        public async Task<IActionResult> Get(string subscriberId, string channel)
        {
            await _notifyService.SubscribeAsync(subscriberId, channel);
            return Ok();
        }
    }
}
