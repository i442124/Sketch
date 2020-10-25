using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Sketch.WebApp.Areas.Tools
{
    public partial class SKColor
    {
        private async Task OnClickAsync(MouseEventArgs e)
        {
            await ColorSelected.InvokeAsync(Color);
        }
    }
}
