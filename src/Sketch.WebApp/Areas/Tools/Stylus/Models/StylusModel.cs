using System;
using System.Threading;
using System.Threading.Tasks;

namespace Sketch.WebApp.Areas.Tools
{
    public class StylusModel : IStylusModel
    {
        public float Size { get; private set; }

        public Task SetSizeAsync(float size)
        {
            return Task.Run(() => Size = size);
        }
    }
}
