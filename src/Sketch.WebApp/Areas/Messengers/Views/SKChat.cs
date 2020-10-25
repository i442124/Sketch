using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

using Sketch;
using Sketch.Shared;

namespace Sketch.WebApp.Areas.Messaging
{
    public partial class SKChat : SKChatComponent
    {
        [Parameter]
        public string Name { get; set; }

        [Parameter]
        public string Contents { get; set; }

        private async Task OnClickAsync(MouseEventArgs e)
        {
            await SendAsync(new Message
            {
                Name = Name,
                Contents = Contents
            });

            Contents = string.Empty;
            await InvokeAsync(StateHasChanged);
        }
    }
}
