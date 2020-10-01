namespace Sketch.Shared
{
    public struct Line
    {
        public Point End { get; set; }

        public Point Start { get; set; }

        public static implicit operator Line((Point Start, Point End) line)
        {
            return new Line { Start = line.Start, End = line.End };
        }

        public static implicit operator Line((int StartX, int StartY, int EndX, int EndY) line)
        {
            return new Line { Start = (line.StartX, line.StartY), End = (line.EndX, line.EndY) };
        }
    }
}
