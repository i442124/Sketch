using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Sketch.Shared;
using Sketch.WebServer;
using Sketch.WebServer.Services;

namespace Sketch.WebServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WhiteboardController : Controller
    {
        private readonly IWhiteboardService _whiteboardService;

        public WhiteboardController(IWhiteboardService notifyService)
        {
            _whiteboardService = notifyService;
        }

        [HttpPost("{channel}")]
        public async Task<IActionResult> DrawAsync(string channel, [FromBody] Stroke stroke)
        {
            await _whiteboardService.DrawAsync(channel, stroke);
            return Ok();
        }
    }
}
