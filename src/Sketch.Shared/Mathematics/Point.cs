namespace Sketch.Shared
{
    public struct Point
    {
        public int X { get; set; }

        public int Y { get; set; }

        public static implicit operator Point((int X, int Y) point)
        {
            return new Point { X = point.X, Y = point.Y };
        }
    }
}
