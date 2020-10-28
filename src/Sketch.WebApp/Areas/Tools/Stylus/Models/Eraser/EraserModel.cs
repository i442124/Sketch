using System;
using Sketch.Shared;
using Sketch.WebApp.Areas.Tools;

namespace Sketch.WebApp.Areas.Tools
{
    public class EraserModel : IEraserModel
    {
        private readonly IStylusTipModel _stylusTip;

        public float Size
        {
            get { return _stylusTip.Size; }
        }

        public float Opacity
        {
            get { throw new NotImplementedException(); }
        }

        public EraserModel(IStylusTipModel stylusTip)
        {
            _stylusTip = stylusTip;
        }
    }
}
