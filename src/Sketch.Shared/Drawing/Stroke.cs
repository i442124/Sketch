namespace Sketch.Shared
{
    public class Stroke
    {
        public Line Line { get; set; }

        public StrokeOptions Options { get; set; }

        public Stroke()
        {
        }

        public Stroke(Line line, StrokeOptions options)
        {
            (Line, Options) = (line, options);
        }

        public Stroke(Point start, Point end, StrokeOptions options)
            : this((start, end), options)
        {
        }

        public Stroke(int startX, int startY, int endX, int endY, StrokeOptions options)
            : this((startX, startY), (endX, endY), options)
        {
        }
    }
}
