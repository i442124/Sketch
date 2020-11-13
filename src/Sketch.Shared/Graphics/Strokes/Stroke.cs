namespace Sketch.Shared
{
    public class Stroke : Drawable
    {
        public StrokeStyle Style { get; set; }

        public StylusPointCollection StylusPoints { get; set; }
    }
}
