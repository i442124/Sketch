using System.Threading.Tasks;

namespace Sketch.Shared.Models
{
    public class BrushSettings : IBrushSettings
    {
        public float Size { get; private set; }

        public float Opacity { get; private set; }

        public void SetSize(float size)
        {
            Size = size;
        }

        public Task SetSizeAsync(float size)
        {
            return Task.Run(() => SetSize(size));
        }

        public void SetOpacity(float opacity)
        {
            Opacity = opacity;
        }

        public Task SetOpacityAsync(float opacity)
        {
            return Task.Run(() => SetOpacity(opacity));
        }
    }
}
