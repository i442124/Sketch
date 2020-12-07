namespace Sketch.Shared.Data.Ink
{
    public class Fill : Event
    {
        public FillStyle Style { get; set; }

        public StylusPoint StylusPoint { get; set; }
    }
}
