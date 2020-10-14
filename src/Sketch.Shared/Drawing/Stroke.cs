namespace Sketch.Shared
{
    public class Stroke
    {
        public Line Line { get; set; }

        public StrokeOptions Options { get; set; }

        public Point End => Line.End;

        public int EndY => Line.End.Y;

        public int EndX => Line.End.X;

        public Point Start => Line.Start;

        public int StartX => Line.Start.X;

        public int StartY => Line.Start.Y;
    }
}
