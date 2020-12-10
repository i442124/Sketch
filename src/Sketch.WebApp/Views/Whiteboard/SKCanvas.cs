using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

using Sketch.Shared.Data;
using Sketch.Shared.Data.Ink;
using Sketch.Shared.Models;

using Sketch.WebApp;
using Sketch.WebApp.Components;

namespace Sketch.WebApp.Views
{
    public partial class SKCanvas : SKCanvasComponent
    {
        private int _mouseX;
        private int _mouseY;
        private bool _painting;

        private async Task OnMouseUp(MouseEventArgs e)
        {
            if (_painting && !IsPrimaryButtonPressed(e))
            {
                _painting = false;
                await InvokeActionChanged();
            }
        }

        private async Task OnMouseDown(MouseEventArgs e)
        {
            var currentX = (int)e.OffsetX;
            var currentY = (int)e.OffsetY;

            var previousX = _mouseX;
            var previousY = _mouseY;

            if (!_painting && IsPrimaryButtonPressed(e))
            {
                _painting = true;
                await InvokeActionChanged();
                await DrawAsync(previousX, previousY, currentX, currentY);
            }
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

            _mouseX = currentX;
            _mouseY = currentY;

            if (_painting = IsPrimaryButtonPressed(e))
            {
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
            if (StylusSettings.Mode == StylusMode.Brush)
            {
                var stroke = new Stroke
                {
                    Style = new StrokeStyle
                    {
                        Color = Brush.Color,
                        Thickness = Brush.Size
                    },
                    StylusPoints = new StylusPointCollection
                    {
                        (previousX, previousY), (currentX, currentY)
                    }
                };

                await StrokeAsync(stroke);
            }
            else if (StylusSettings.Mode == StylusMode.Eraser)
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

                await WipeAsync(wipe);
            }
            else if (StylusSettings.Mode == StylusMode.PaintBucket)
            {
                var fill = new Fill
                {
                    Style = new FillStyle
                    {
                        Color = PaintBucket.Color
                    },
                    StylusPoint = new StylusPoint
                    {
                        X = currentX, Y = currentY
                    }
                };

                await FillAsync(fill);
            }
        }

        [Inject]
        private IBrushTool Brush { get; set; }

        [Inject]
        private IEraserTool Eraser { get; set; }

        [Inject]
        private IPaintBucketTool PaintBucket { get; set; }

        [Inject]
        private IStylusSettings StylusSettings { get; set; }
    }
}
