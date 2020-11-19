using System;

namespace Sketch.WebApp.Components
{
    public class EraserModel : IEraserModel
    {
        private readonly ISizeObjectModel _sizeObject;

        public EraserModel(ISizeObjectModel sizeObject)
        {
            _sizeObject = sizeObject;
        }

        public float Size => _sizeObject.Size;

        public float Opacity => throw new NotImplementedException();
    }
}
