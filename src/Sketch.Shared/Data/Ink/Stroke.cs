namespace Sketch.Shared.Data.Ink
{
    public class Stroke : Event
    {
        public StrokeStyle Style { get; set; }

        public StylusPointCollection StylusPoints { get; set; }
    }
}
