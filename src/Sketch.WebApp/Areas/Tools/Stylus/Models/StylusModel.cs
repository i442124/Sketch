using System;
using System.Threading.Tasks;

using Sketch.Shared;
using Sketch.WebApp.Areas.Whiteboard;

namespace Sketch.WebApp.Areas.Tools
{
    public class StylusModel : IStylusModel
    {
        public StylusMode Mode { get; private set; }

        public StylusModel()
        {
        }

        public Task UseBrushAsync()
        {
            return Task.Run(() => Mode = StylusMode.Brush);
        }

        public Task UseEraserAsync()
        {
            return Task.Run(() => Mode = StylusMode.Erase);
        }

        public Task UseBucketAsync()
        {
            return Task.Run(() => Mode = StylusMode.Fill);
        }
    }
}
