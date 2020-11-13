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
        private bool _painting;
        private int _previousX;
        private int _previousY;

        private void OnMouseUp(MouseEventArgs e)
        {
            _painting = IsPrimaryButtonPressed(e);
        }

        private void OnMouseOver(MouseEventArgs e)
        {
            _previousX = (int)e.OffsetX;
            _previousY = (int)e.OffsetY;
            _painting = IsPrimaryButtonPressed(e);
        }

        private async Task OnMouseDown(MouseEventArgs e)
        {
            if (_painting = IsPrimaryButtonPressed(e))
            {
                var currentX = (int)e.OffsetX;
                var currentY = (int)e.OffsetY;
                await InvokeWhiteboardActionChanged();
                await DrawAsync(_previousX, _previousY, currentX, currentY);
            }
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
                await DrawAsync((previousX, previousY), (currentX, currentY));
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

        private async Task DrawAsync(Point previous, Point current)
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

                await StrokeAsync(stroke, stroke.Style);
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

                await WipeAsync(wipe, wipe.Style);
                await SendAsync(wipe);
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

                await FillAsync(fill, fill.Style);
                await SendAsync(fill);
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
