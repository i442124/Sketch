using System.Threading.Tasks;
using Sketch.Shared;

namespace Sketch.WebApp.Models.Toolbox
{
    public class BrushModel : IBrushModel
    {
        private readonly IPipetteModel _pipette;

        public Color Color
        {
            get { return _pipette.Color; }
        }

        public float Size { get; private set; }

        public float Opacity { get; private set; }

        public BrushModel(IPipetteModel pipette)
        {
            _pipette = pipette;
        }

        public Task SetSizeAsync(float size)
        {
            return Task.Run(() => Size = size);
        }

        public Task SetColorAsync(Color color)
        {
            return _pipette.SetColorAsync(color);
        }

        public Task SetOpacityAsync(float opacity)
        {
            return Task.Run(() => Opacity = opacity);
        }
    }
}
