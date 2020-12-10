namespace Sketch.Shared.Data.Ink
{
    public class Wipe : Event
    {
        public WipeStyle Style { get; set; }

        public StylusPointCollection StylusPoints { get; set; }
    }
}
