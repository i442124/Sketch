using System.Threading;
using System.Threading.Tasks;

using Sketch.WebApp;
using Sketch.WebApp.Components;

namespace Sketch.WebApp.Views
{
    public partial class SKBrushButton
    {
        private Task OnClickAsync()
        {
            return UseBrushAsync();
        }
    }
}
