using System;

namespace Sketch.Shared
{
    public abstract class Drawable
    {
        public string ActionId { get; set; }

        public Drawable() => ActionId = Guid.NewGuid().ToString();
    }
}
