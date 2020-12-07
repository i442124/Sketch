using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

using Sketch.WebApp;
using Sketch.WebApp.Components;

namespace Sketch.WebApp.Views
{
    public partial class SKClearButton : SKClearComponent
    {
        private Task OnClickAsync(MouseEventArgs e)
        {
            return ClearAsync();
        }
    }
}
