namespace Sketch.Shared
{
    public class Stroke : IAction
    {
        public string ActionId { get; set; }

        public StrokeStyle Style { get; set; }

        public StylusPointCollection StylusPoints { get; set; }
    }
}
