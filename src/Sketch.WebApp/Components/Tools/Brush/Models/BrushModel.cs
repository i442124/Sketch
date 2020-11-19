using System;
using Sketch.Shared;
using Sketch.WebApp.Components;

namespace Sketch.WebApp.Components
{
    public class BrushModel : IBrushModel
    {
        private readonly ISizeObjectModel _sizeObject;
        private readonly IColorObjectModel _colorObject;

        public BrushModel(ISizeObjectModel sizeObject, IColorObjectModel colorObject)
        {
            _sizeObject = sizeObject;
            _colorObject = colorObject;
        }

        public float Size => _sizeObject.Size;

        public Color Color => _colorObject.Color;

        public float Opacity => throw new NotImplementedException();
    }
}
