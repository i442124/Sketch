using System.Threading;
using System.Threading.Tasks;

using Sketch.WebApp;
using Sketch.WebApp.Components;

namespace Sketch.WebApp.Views
{
    public partial class SKEraserButton : SKEraserComponent
    {
        private Task OnClickAsync()
        {
            return UseEraserAsync();
        }
    }
}
