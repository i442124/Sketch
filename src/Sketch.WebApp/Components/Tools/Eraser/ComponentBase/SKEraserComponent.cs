using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Components;

namespace Sketch.WebApp.Components
{
    public abstract class SKEraserComponent : ComponentBase
    {
        public async Task UseAsync()
        {
            await Stylus.UseEraserAsync(Eraser);
        }

        public float Size => Eraser.Size;

        public float Opacity => Eraser.Opacity;

        [Inject]
        private IEraserModel Eraser { get; set; }

        [Inject]
        private IStylusModel Stylus { get; set; }
    }
}
