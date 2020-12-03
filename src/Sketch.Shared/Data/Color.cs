namespace Sketch.Shared.Data
{
    public struct Color
    {
        public byte R { get; private set; }

        public byte G { get; private set; }

        public byte B { get; private set; }

        public double Hue => ColorUtilities.GetHue(this);

        public double Saturation => ColorUtilities.GetSaturation(this);

        public double Luminosity => ColorUtilities.GetLuminosity(this);

        public Color(byte r, byte g, byte b)
        {
            this = new Color { R = r, G = g, B = b };
        }

        public Color(double hue, double saturation, double luminosity)
        {
            this = FromHSL(hue, saturation, luminosity);
        }

        public static Color FromRGB(byte r, byte g, byte b)
        {
            return ColorUtilities.FromRGB(r, g, b);
        }

        public static Color FromHSL(double hue, double saturation, double luminosity)
        {
            return ColorUtilities.FromHSL(hue, saturation, luminosity);
        }
    }
}
