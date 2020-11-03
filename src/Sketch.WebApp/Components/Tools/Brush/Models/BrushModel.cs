using System;
using Sketch.Shared;
using Sketch.WebApp.Components;

namespace Sketch.WebApp.Components
{
    public class BrushModel : IBrushModel
    {
        private readonly IPipetteModel _pipette;
        private readonly IStylusTipModel _stylus;

        public BrushModel(IStylusTipModel stylus, IPipetteModel pipette)
        {
            _stylus = stylus;
            _pipette = pipette;
        }

        public float Size => _stylus.Size;

        public Color Color => _pipette.Color;

        public float Opacity => throw new NotImplementedException();
    }
}
