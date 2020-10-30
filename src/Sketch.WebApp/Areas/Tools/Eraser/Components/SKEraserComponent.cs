using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Components;

using Sketch.Shared;
using Sketch.WebApp.Areas;
using Sketch.WebApp.Areas.Configuration;

namespace Sketch.WebApp.Areas.Tools
{
    public class SKEraserComponent : ComponentBase
    {
        public float Size => Eraser.Size;

        protected async Task UseEraserAsync()
        {
            await Stylus.UseEraserAsync(Eraser);
        }

        [Inject]
        private IEraserModel Eraser { get; set; }

        [Inject]
        private IStylusModel Stylus { get; set; }
    }
}
