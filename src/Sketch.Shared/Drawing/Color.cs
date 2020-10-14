namespace Sketch.Shared
{
    public struct Color
    {
        public byte A { get; set; }

        public byte R { get; set; }

        public byte G { get; set; }

        public byte B { get; set; }

        public string AsHex()
        {
            return $"#{R:X2}{G:X2}{B:X2}{A:X2}";
        }

        public override string ToString()
        {
            return $"R:{R} G:{G} B:{B} A:{A}";
        }

        public static implicit operator System.Drawing.Color(Color color)
        {
            return System.Drawing.Color.FromArgb(color.A, color.R, color.G, color.B);
        }

        public static implicit operator Color(System.Drawing.Color color)
        {
            return new Color { A = color.A, R = color.R, G = color.G, B = color.B };
        }
    }
}
