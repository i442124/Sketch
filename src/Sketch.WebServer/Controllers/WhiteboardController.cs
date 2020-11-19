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

        [HttpPost("{channel}/stroke/{subscriberId}")]
        public async Task<IActionResult> StrokeAsync(string channel, string subscriberId, [FromBody] Stroke stroke)
        {
            await _whiteboardService.StrokeAsync(channel, subscriberId, stroke);
            return Ok();
        }

        [HttpPost("{channel}/wipe/{subscriberId}")]
        public async Task<IActionResult> WipeAsync(string channel, string subscriberId, [FromBody] Wipe wipe)
        {
            await _whiteboardService.WipeAsync(channel, subscriberId, wipe);
            return Ok();
        }

        [HttpPost("{channel}/fill/{subscriberId}")]
        public async Task<IActionResult> FillAsync(string channel, string subscriberId, [FromBody] Fill fill)
        {
            await _whiteboardService.FillAsync(channel, subscriberId, fill);
            return Ok();
        }

        [HttpPost("{channel}/clear/{subscriberId}")]
        public async Task<IActionResult> ClearAsync(string channel, string subscriberId, [FromBody] Clear clear)
        {
            await _whiteboardService.ClearAsync(channel, subscriberId, clear);
            return Ok();
        }

        [HttpPost("{channel}/undo/{subscriberId}")]
        public async Task<IActionResult> UndoAsync(string channel, string subscriberId, [FromBody] Undo undo)
        {
            await _whiteboardService.UndoAsync(channel, subscriberId, undo);
            return Ok();
        }
    }
}
