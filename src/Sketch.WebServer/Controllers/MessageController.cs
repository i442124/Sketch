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
    public class MessageController : Controller
    {
        private readonly IMessengerService _messengerService;

        public MessageController(IMessengerService messengerService)
        {
            _messengerService = messengerService;
        }

        [HttpPost("{channel}")]
        public async Task<IActionResult> SendAysnc(string channel, [FromBody] string message)
        {
            await _messengerService.SendAsync(channel, message);
            return Ok();
        }
    }
}
