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
    public class RegisterController : Controller
    {
        private readonly INotificationService _notifyService;
        private readonly IHubConnectionMapper<User> _connections;

        public RegisterController(
            INotificationService notifyService,
            IHubConnectionMapper<User> connections)
        {
            _connections = connections;
            _notifyService = notifyService;
        }

        [HttpGet("{subscriberId}/{name}")]
        public async Task<IActionResult> RegisterAsync(string subscriberId, string name)
        {
            var user = await _connections.GetUserInfoAsync(subscriberId);
            if (user != null)
            {
                await _notifyService.UpdateAsync(subscriberId, new User
                {
                    Name = name, Guid = user.Guid
                });

                return Ok();
            }

            return NotFound();
        }
    }
}
