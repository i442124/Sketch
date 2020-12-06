namespace Sketch.Shared.Data.Ink
{
    public class Stroke : Action
    {
        public StrokeStyle Style { get; set; }

        public StylusPointCollection StylusPoints { get; set; }
    }
}
