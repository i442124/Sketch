using System;
using Sketch.Shared;

namespace Sketch.WebApp.Components
{
    public class BucketModel : IBucketModel
    {
        private readonly IPipetteModel _pipette;

        public BucketModel(IPipetteModel pipette)
        {
            _pipette = pipette;
        }

        public Color Color => _pipette.Color;

        public float Opacity => throw new NotImplementedException();
    }
}
