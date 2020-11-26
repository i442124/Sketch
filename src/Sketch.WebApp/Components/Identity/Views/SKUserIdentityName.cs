using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Components;

namespace Sketch.WebApp.Components
{
    public partial class SKUserIdentityName : SKUserIdentityComponent
    {
        private async Task OnInputChangedAsync(ChangeEventArgs e)
        {
            await SetNameAsync(e.Value.ToString());
        }
    }
}
