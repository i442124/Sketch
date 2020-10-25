using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Components;

namespace Sketch.WebApp.Areas.Subscriptions
{
    public partial class SKChannel : SKChannelComponent
    {
        private Task OnSelectionChanged(ChangeEventArgs e)
        {
            return SubscribeAsync((string)e.Value);
        }
    }
}
