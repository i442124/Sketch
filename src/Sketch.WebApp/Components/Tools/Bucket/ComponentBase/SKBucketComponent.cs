using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Components;

using Sketch.Shared;
using Sketch.WebApp.Components;

namespace Sketch.WebApp.Components
{
    public abstract class SKBucketComponent : ComponentBase
    {
        public async Task UseAsync()
        {
            await Stylus.UseBucketAsync(Bucket);
        }

        public Color Color => Bucket.Color;

        public float Opacity => Bucket.Opacity;

        [Inject]
        private IBucketModel Bucket { get; set; }

        [Inject]
        private IStylusModel Stylus { get; set; }
    }
}
