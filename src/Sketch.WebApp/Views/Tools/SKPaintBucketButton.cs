using System.Threading;
using System.Threading.Tasks;

using Sketch.WebApp;
using Sketch.WebApp.Components;

namespace Sketch.WebApp.Views
{
    public partial class SKPaintBucketButton
    {
        private Task OnClickAsync()
        {
            return UsePaintBucketAsync();
        }
    }
}
