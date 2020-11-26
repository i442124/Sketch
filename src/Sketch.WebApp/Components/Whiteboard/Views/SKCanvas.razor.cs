using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

using Sketch.Shared;
using Sketch.WebApp.Components;

namespace Sketch.WebApp.Components
{
    public partial class SKCanvas : SKCanvasComponent
    {
        private int _mouseX;

        private int _mouseY;

        private bool _painting;

        private async Task OnMouseUp(MouseEventArgs e)
        {
            _painting = IsPrimaryButtonPressed(e);
            await Task.CompletedTask;
        }

        private async Task OnMouseMove(MouseEventArgs e)
        {
            var currentX = (int)e.OffsetX;
            var currentY = (int)e.OffsetY;

            var previousX = _mouseX;
            var previousY = _mouseY;

            _mouseX = currentX;
            _mouseY = currentY;

            if (_painting)
            {
                await DrawAsync(previousX, previousY, currentX, currentY);
            }
        }

        private async Task OnMouseOver(MouseEventArgs e)
        {
            var currentX = (int)e.OffsetX;
            var currentY = (int)e.OffsetY;

            var previousX = _mouseX;
            var previousY = _mouseY;

            if (_painting = IsPrimaryButtonPressed(e))
            {
                await DrawAsync(previousX, previousY, currentX, currentY);
            }
        }

        private async Task OnMouseDown(MouseEventArgs e)
        {
            var currentX = (int)e.OffsetX;
            var currentY = (int)e.OffsetY;

            var previousX = _mouseX;
            var previousY = _mouseY;

            if (_painting = IsPrimaryButtonPressed(e))
            {
                await InvokeActionChanged();
                await DrawAsync(previousX, previousY, currentX, currentY);
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

        private async Task DrawAsync(StylusPoint previous, StylusPoint current)
        {
            await DrawAsync(previous.X, previous.Y, current.X, current.Y);
        }

        private async Task DrawAsync(int previousX, int previousY, int currentX, int currentY)
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

                await InvokeStrokeAsync(stroke);
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

                await InvokeWipeAsync(wipe);
            }
            else if (Stylus.Mode == StylusMode.Fill)
            {
                var fill = new Fill
                {
                    Style = new FillStyle
                    {
                        Color = Bucket.Color
                    },
                    StylusPoint = new StylusPoint
                    {
                        X = currentX, Y = currentY
                    }
                };

                await InvokeFillAsync(fill);
            }
        }

        [Inject]
        private IBrushModel Brush { get; set; }

        [Inject]
        private IEraserModel Eraser { get; set; }

        [Inject]
        private IBucketModel Bucket { get; set; }

        [Inject]
        private IStylusModel Stylus { get; set; }
    }
}
