using System;

using Sketch;
using Sketch.Shared;
using Sketch.WebApp.Areas.Tools;

namespace Sketch.WebApp.Areas.Toolbox
{
    public class BrushModel : IBrushModel
    {
        private readonly IStylusModel _stylus;
        private readonly IPipetteModel _pipette;

        public float Size
        {
            get { return _stylus.Size; }
        }

        public Color Color
        {
            get { return _pipette.Color; }
        }

        public float Opacity
        {
            get { throw new NotImplementedException(); }
        }

        public BrushModel(IStylusModel stylus, IPipetteModel pipette)
        {
            _stylus = stylus;
            _pipette = pipette;
        }
    }
}
