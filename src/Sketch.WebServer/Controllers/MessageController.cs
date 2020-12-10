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
    public class MessageController : Controller
    {
        private readonly IMessengerService _messengerService;

        public MessageController(IMessengerService messengerService)
        {
            _messengerService = messengerService;
        }

        [HttpPost("{subscriberId}")]
        public async Task<IActionResult> MessageAsync(string subscriberId, [FromBody] Message message)
        {
            await _messengerService.SendAsync(subscriberId, message);
            return Ok();
        }
    }
}
