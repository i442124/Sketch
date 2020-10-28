using System;
using Sketch.Shared;
using Sketch.WebApp.Areas.Tools;

namespace Sketch.WebApp.Areas.Tools
{
    public class BrushModel : IBrushModel
    {
        private readonly IPipetteModel _pipette;
        private readonly IStylusTipModel _stylusTip;

        public float Size
        {
            get { return _stylusTip.Size; }
        }

        public Color Color
        {
            get { return _pipette.Color; }
        }

        public float Opacity
        {
            get { throw new NotImplementedException(); }
        }

        public BrushModel(IStylusTipModel stylusTip, IPipetteModel pipette)
        {
            _stylusTip = stylusTip;
            _pipette = pipette;
        }
    }
}
