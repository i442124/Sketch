using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc;

using Sketch.Shared;
using Sketch.Shared.Data;
using Sketch.Shared.Data.Ink;
using Sketch.WebServer.Services;

namespace Sketch.WebServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WhiteboardController : Controller
    {
        private readonly IWhiteboardService _whiteboardService;

        public WhiteboardController(IWhiteboardService whiteboardService)
        {
            _whiteboardService = whiteboardService;
        }

        [HttpPost("stroke/{subscriberId}")]
        public async Task<IActionResult> StrokeAsync(string subscriberId, [FromBody] Stroke stroke)
        {
            await _whiteboardService.StrokeAsync(subscriberId, stroke);
            return Ok();
        }

        [HttpPost("wipe/{subscriberId}")]
        public async Task<IActionResult> WipeAsync(string subscriberId, [FromBody] Wipe wipe)
        {
            await _whiteboardService.WipeAsync(subscriberId, wipe);
            return Ok();
        }

        [HttpPost("fill/{subscriberId}")]
        public async Task<IActionResult> FillAsync(string subscriberId, [FromBody] Fill fill)
        {
            await _whiteboardService.FillAsync(subscriberId, fill);
            return Ok();
        }

        [HttpPost("clear/{subscriberId}")]
        public async Task<IActionResult> ClearAsync(string subscriberId, [FromBody] Clear clear)
        {
            await _whiteboardService.ClearAsync(subscriberId, clear);
            return Ok();
        }

        [HttpPost("undo/{subscriberId}")]
        public async Task<IActionResult> UndoAsync(string subscriberId, [FromBody] Undo undo)
        {
            await _whiteboardService.UndoAsync(subscriberId, undo);
            return Ok();
        }
    }
}
