using System.Threading;
using System.Threading.Tasks;

namespace Sketch.WebApp.Components
{
    public class SizeObjectModel : ISizeObjectModel
    {
        public float Size { get; private set; }

        public async Task SetSizeAsync(float newSize)
        {
            await Task.Run(() => Size = newSize);
        }
    }
}
