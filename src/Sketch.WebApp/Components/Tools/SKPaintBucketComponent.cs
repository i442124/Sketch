using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Components;

using Sketch.Shared.Data;
using Sketch.Shared.Data.Ink;
using Sketch.Shared.Data.Ink.Colors;
using Sketch.Shared.Models;

namespace Sketch.WebApp.Components
{
    public abstract class SKPaintBucketComponent : ComponentBase
    {
        public Color Color
        {
            get { return PaintBucket.Color; }
        }

        public float Opacity
        {
            get { return PaintBucket.Opacity; }
        }

        public void UsePaintBucket()
        {
            StylusSettings.UsePaintBucket(PaintBucket);
        }

        public async Task UsePaintBucketAsync()
        {
            await StylusSettings.UsePaintBucketAsync(PaintBucket);
        }

        [Inject]
        private IPaintBucketTool PaintBucket { get; set; }

        [Inject]
        private IStylusSettings StylusSettings { get; set; }
    }
}
