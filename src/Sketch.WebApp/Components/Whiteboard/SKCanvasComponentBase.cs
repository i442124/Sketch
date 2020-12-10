using System.Threading;
using System.Threading.Tasks;

using Excubo.Blazor.Canvas;
using Excubo.Blazor.Canvas.Contexts;

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Sketch.WebApp.Components
{
    public abstract class SKCanvasComponentBase : ComponentBase
    {
        #pragma warning disable SA1401
        protected ElementReference _canvasRef;

        [Parameter]
        public int Width { get; set; }

        [Parameter]
        public int Height { get; set; }

        public Task<Context2D> CreateCanvas2DAsync()
        {
            return JSRuntime.GetContext2DAsync(_canvasRef);
        }

        public Task<Context2D> CreateCanvas2DAsync(bool alpha)
        {
            return JSRuntime.GetContext2DAsync(_canvasRef, alpha);
        }

        public Task<Context2D> CreateCanvas2DAsync(bool alpha, bool desynchronized)
        {
            return JSRuntime.GetContext2DAsync(_canvasRef, alpha, desynchronized);
        }

        [Inject]
        private IJSRuntime JSRuntime { get; set; }
    }
}
