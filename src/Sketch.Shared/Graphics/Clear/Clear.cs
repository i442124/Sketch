namespace Sketch.Shared
{
    public class Clear : Drawable
    {
        public static Clear All { get; } = new Clear
        {
            Width = int.MaxValue, Height = int.MaxValue
        };

        public int X { get; set; }

        public int Y { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }
    }
}
