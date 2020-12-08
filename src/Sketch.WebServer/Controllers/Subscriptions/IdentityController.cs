using System.Threading.Tasks;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc;

using Sketch.Shared.Data;
using Sketch.WebServer.Hubs;
using Sketch.WebServer.Services;

namespace Sketch.WebServer.Controllers.Subscriptions
{
    [ApiController]
    [Route("api/[controller]")]
    public class IdentityController : Controller
    {
        private readonly IBroadcastService _broadcastService;
        private readonly IHubConnectionMapper<User> _connections;

        public IdentityController(
            IBroadcastService broadcastService,
            IHubConnectionMapper<User> connections)
        {
            _connections = connections;
            _broadcastService = broadcastService;
        }

        [HttpGet("{subscriberId}/{name}")]
        public async Task<IActionResult> IdentifyAsync(string subscriberId, string name)
        {
            var user = await _connections.GetConnectionInfoAsync(subscriberId);
            if (user != null)
            {
                await _broadcastService.IdentifyAsync(subscriberId, new User
                {
                    Name = name, Guid = user.Guid
                });

                return Ok();
            }

            return NotFound();
        }
    }
}
