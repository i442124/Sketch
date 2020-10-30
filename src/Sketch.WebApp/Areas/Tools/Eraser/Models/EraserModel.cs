using System;
using Sketch.Shared;
using Sketch.WebApp.Areas;
using Sketch.WebApp.Areas.Configuration;

namespace Sketch.WebApp.Areas.Tools
{
    public class EraserModel : IEraserModel
    {
        private readonly IStylusTipModel _stylus;

        public EraserModel(IStylusTipModel stylus)
        {
            _stylus = stylus;
        }

        public float Size => _stylus.Size;

        public float Opacity => throw new NotImplementedException();
    }
}
