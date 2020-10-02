using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

using Sketch.Shared;
using Sketch.WebApp;
using Sketch.WebApp.Models;

namespace Sketch.WebApp.Components
{
    public abstract class SKChannelComponent : ComponentBase
    {
        [Inject]
        public ISubscriptionModel Subscription { get; set; }
    }
}
