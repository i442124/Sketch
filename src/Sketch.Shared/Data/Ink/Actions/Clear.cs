namespace Sketch.Shared.Data
{
    public class Clear : Event
    {
        public static Clear All => new Clear
        {
            Width = int.MaxValue, Height = int.MaxValue
        };

        public int X { get; set; }

        public int Y { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }
    }
}
