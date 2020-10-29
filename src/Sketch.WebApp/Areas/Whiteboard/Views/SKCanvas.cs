using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

using Sketch;
using Sketch.Shared;
using Sketch.WebApp.Areas;
using Sketch.WebApp.Areas.Tools;

namespace Sketch.WebApp.Areas.Whiteboard
{
    public partial class SKCanvas : SKCanvasComponent
    {
        private bool _painting;
        private int _previousX;
        private int _previousY;

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
                if (Stylus.Mode == StylusMode.Brush)
                {
                    var stroke = new Stroke
                    {
                        Style = new StrokeStyle
                        {
                            Color = Brush.Color, Thickness = Brush.Size
                        },
                        StylusPoints = new StylusPointCollection
                        {
                            (previousX, previousY), (currentX, currentY)
                        }
                    };

                    await SendAsync(stroke);
                }
                else if (Stylus.Mode == StylusMode.Erase)
                {
                    var wipe = new Wipe
                    {
                        Style = new WipeStyle
                        {
                            Thickness = Eraser.Size
                        },
                        StylusPoints = new StylusPointCollection
                        {
                            (previousX, previousY), (currentX, currentY)
                        }
                    };

                    await SendAsync(wipe);
                }
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

        [Inject]
        private IEraserModel Eraser { get; set; }

        [Inject]
        private IStylusModel Stylus { get; set; }
    }
}
