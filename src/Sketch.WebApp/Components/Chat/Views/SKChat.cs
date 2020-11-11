using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Sketch.WebApp.Components
{
    public partial class SKChat : SKChatComponent
    {
        private async Task OnClickAsync(MouseEventArgs e)
        {
            await SendAsync(clearAfterSend: true);
        }
    }
}
