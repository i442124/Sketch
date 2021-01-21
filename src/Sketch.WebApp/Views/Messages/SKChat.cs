using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

using Sketch.Shared.Data;
using Sketch.Shared.Models;
using Sketch.WebApp.Components;

namespace Sketch.WebApp.Views
{
    public partial class SKChat : SKChatComponent
    {
        [Parameter]
        public bool ClearAfterSend { get; set; }

        public string Contents { get; set; }

        private async Task OnClickAsync(MouseEventArgs e)
        {
            if (!string.IsNullOrEmpty(Contents))
            {
                await SendAsync(Contents);

                if (ClearAfterSend)
                {
                    Contents = string.Empty;
                    await InvokeAsync(StateHasChanged);
                }
            }
        }
    }
}
