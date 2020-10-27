using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

using Sketch;
using Sketch.Shared;
using Sketch.WebApp.Areas.Toolbox;

namespace Sketch.WebApp.Areas.Whiteboard
{
    public partial class SKCanvas : SKCanvasComponent
    {
        private bool _painting;
        private int _previousX;
        private int _previousY;

        public Color Color => Brush.Color;

        public float Thickness => Brush.Size;

        private void OnMouseInput(MouseEventArgs e)
        {
            _previousX = (int)e.OffsetX;
            _previousY = (int)e.OffsetY;
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
                    Style = new StrokeStyle
                    {
                        Color = Color, Thickness = Thickness
                    },
                    StylusPoints = new StylusPointCollection
                    {
                        (previousX, previousY), (currentX, currentY)
                    }
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

        [Inject]
        private IBrushModel Brush { get; set; }
    }
}
