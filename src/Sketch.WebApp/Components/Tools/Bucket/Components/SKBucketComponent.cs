using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Components;

using Sketch.Shared;
using Sketch.WebApp.Components;

namespace Sketch.WebApp.Components
{
    public class SKBucketComponent : ComponentBase
    {
        public Color Color => Bucket.Color;

        public float Opacity => Bucket.Opacity;

        protected Task UseBucketAsync()
        {
            return Stylus.UseBucketAsync(Bucket);
        }

        [Inject]
        private IBucketModel Bucket { get; set; }

        [Inject]
        private IStylusModel Stylus { get; set; }
    }
}
