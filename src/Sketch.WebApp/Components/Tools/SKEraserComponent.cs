using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Components;

using Sketch.Shared;
using Sketch.Shared.Data;
using Sketch.Shared.Models;

namespace Sketch.WebApp.Components
{
    public abstract class SKEraserComponent : ComponentBase
    {
        public float Size
        {
            get { return Eraser.Size; }
        }

        public float Opacity
        {
            get { return Eraser.Opacity; }
        }

        public void UseEraser()
        {
            StylusSettings.UseEraser(Eraser);
        }

        public async Task UseEraserAsync()
        {
            await StylusSettings.UseEraserAsync(Eraser);
        }

        [Inject]
        private IEraserTool Eraser { get; set; }

        [Inject]
        private IStylusSettings StylusSettings { get; set; }
    }
}
