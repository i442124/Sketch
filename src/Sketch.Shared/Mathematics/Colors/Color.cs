namespace Sketch.Shared
{
    public struct Color
    {
        public byte R { get; set; }

        public byte G { get; set; }

        public byte B { get; set; }

        public Color(byte r, byte g, byte b)
        {
            this = FromRGB(r, g, b);
        }

        public Color(double hue, double saturation, double luminosity)
        {
            this = FromHSL(hue, saturation, luminosity);
        }

        public double Hue
        {
            get { return ColorUtils.GetHue(this); }
            set { this = FromHSL(value, Saturation, Luminosity); }
        }

        public double Saturation
        {
            get { return ColorUtils.GetSaturation(this); }
            set { this = FromHSL(Hue, value, Luminosity); }
        }

        public double Luminosity
        {
            get { return ColorUtils.GetLuminosity(this); }
            set { this = FromHSL(Hue, Saturation, value); }
        }

        public string ToRGBString()
        {
            return $"R: {R} G: {G} B: {B}";
        }

        public string ToHexString()
        {
            return $"#{R:X2}{G:X2}{B:X2}";
        }

        public string ToHSLString()
        {
            return $"H: {Hue} S: {Saturation} L:{Luminosity}";
        }

        public override string ToString()
        {
            return $"R: {R} G: {G} B: {B} H: {Hue} S: {Saturation}: L: {Luminosity}";
        }

        public static Color FromRGB(byte r, byte g, byte b)
        {
            return ColorUtils.FromRGB(r, g, b);
        }

        public static Color FromHSL(double hue, double saturation, double luminosity)
        {
            return ColorUtils.FromHLS(hue, saturation, luminosity);
        }
    }
}
