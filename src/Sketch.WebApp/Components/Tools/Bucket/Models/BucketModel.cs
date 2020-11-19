using System;
using Sketch.Shared;

namespace Sketch.WebApp.Components
{
    public class BucketModel : IBucketModel
    {
        private readonly IColorObjectModel _colorObject;

        public BucketModel(IColorObjectModel colorObject)
        {
            _colorObject = colorObject;
        }

        public Color Color => _colorObject.Color;

        public float Opacity => throw new NotImplementedException();
    }
}
