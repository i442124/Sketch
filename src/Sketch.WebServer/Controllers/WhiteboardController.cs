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

        [HttpPost("{channel}/stroke")]
        public async Task<IActionResult> StrokeAsync(string channel, [FromBody] Stroke stroke)
        {
            await _whiteboardService.StrokeAsync(channel, stroke);
            return Ok();
        }

        [HttpPost("{channel}/wipe")]
        public async Task<IActionResult> WipeAsync(string channel, [FromBody] Wipe wipe)
        {
            await _whiteboardService.WipeAsync(channel, wipe);
            return Ok();
        }

        [HttpPost("{channel}/fill")]
        public async Task<IActionResult> FillAsync(string channel, [FromBody] Fill fill)
        {
            await _whiteboardService.FillAsync(channel, fill);
            return Ok();
        }

        [HttpPost("{channel}/clear")]
        public async Task<IActionResult> ClearAsync(string channel, [FromBody] Clear clear)
        {
            await _whiteboardService.ClearAsync(channel, clear);
            return Ok();
        }
    }
}
