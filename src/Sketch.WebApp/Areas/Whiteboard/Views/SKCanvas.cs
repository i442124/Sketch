using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

using Sketch;
using Sketch.Shared;

namespace Sketch.WebApp.Areas.Whiteboard
{
    public partial class SKCanvas : SKCanvasComponent
    {
        private bool _painting;
        private int _previousX;
        private int _previousY;

        [Parameter]
        public Color Color { get; set; }

        [Parameter]
        public float Thickness { get; set; }

        private void OnMouseMount(MouseEventArgs e)
        {
            _painting = IsPrimaryButtonPressed(e);
        }

        private async Task OnMouseMove(MouseEventArgs e)
        {
            var currentX = (int)e.OffsetX;
            var currentY = (int)e.OffsetY;

            var previousX = _previousX;
            var previousY = _previousY;

            _previousX = currentX;
            _previousY = currentY;

            if (_painting)
            {
                var stroke = new Stroke
                {
                    Line = (previousX, previousY, currentX, currentY),
                    Options = new StrokeOptions { Color = Color, Thickness = Thickness }
                };

                await SendAsync(stroke);
            }
        }

        private bool IsPrimaryButtonPressed(MouseEventArgs e)
        {
            return e.Buttons > 0 && ((e.Buttons | 1) == 1);
        }

        private bool IsSecondaryButtonPressed(MouseEventArgs e)
        {
            return e.Buttons > 0 && ((e.Buttons | 2) == 2);
        }
    }
}
