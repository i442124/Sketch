using System;
using System.Threading;
using System.Threading.Tasks;

namespace Sketch.WebApp.Areas.Configuration
{
    public class StylusTipModel : IStylusTipModel
    {
        public float Size { get; private set; }

        public Task SetSizeAsync(float size)
        {
            return Task.Run(() => Size = size);
        }
    }
}
