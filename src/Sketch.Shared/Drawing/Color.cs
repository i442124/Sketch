using System;

namespace Sketch.Shared
{
    public struct Color
    {
        public byte R { get; set; }

        public byte G { get; set; }

        public byte B { get; set; }

        public Color(byte r, byte g, byte b)
        {
            R = G = B = default;
            SetRGBValue(r, g, b);
        }

        public Color(double hue, double saturation, double luminosity)
        {
            R = G = B = default;
            SetHSLValue(hue, saturation, luminosity);
        }

        public double Hue
        {
            get { return GetHue(); }
            set { SetHSLValue(value, Saturation, Luminosity); }
        }

        public double Saturation
        {
            get { return GetSaturation(); }
            set { SetHSLValue(Hue, value, Luminosity); }
        }

        public double Luminosity
        {
            get { return GetLuminosity(); }
            set { SetHSLValue(Hue, Saturation, value); }
        }

        public string ToRGBString()
        {
            return $"R: {R} G: {G} B:{B}";
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
            return $"R: {R} G: {G} B:{B} H: {Hue} S: {Saturation}: L: {Luminosity}";
        }

        public void SetRGBValue(byte r, byte g, byte b)
        {
            (R, G, B) = (r, g, b);
        }

        public void SetHSLValue(double hue, double saturation, double luminosity)
        {
            if (saturation == 0)
            {
                R = G = B = (byte)(luminosity * 255.0);
            }
            else
            {
                double v1, v2;
                v2 = (luminosity < 0.5)
                    ? (luminosity * (1 + saturation))
                    : ((luminosity + saturation) - (luminosity * saturation));

                v1 = (2 * luminosity) - v2;
                hue /= 360;

                R = (byte)(255 * GetColorComponent(v1, v2, hue + (1.0f / 3)));
                G = (byte)(255 * GetColorComponent(v1, v2, hue));
                B = (byte)(255 * GetColorComponent(v1, v2, hue - (1.0f / 3)));
            }
        }

        private double GetHue()
        {
            if (R == G && G == B)
            {
                return 0.0;
            }

            var min = GetMinColorComponent();
            var max = GetMaxColorComponent();

            var hue = 0.0;
            var delta = max - min;

            if (R == max)
            {
                hue = (G - B) / delta;
            }
            else if (G == max)
            {
                hue = 2 + ((B - R) / delta);
            }
            else if (B == max)
            {
                hue = 4 + ((R - G) / delta);
            }

            hue *= 60.0;
            hue = hue < 0.0 ? hue + 360.0 : hue;
            return hue;
        }

        private double GetSaturation()
        {
            var min = GetMinColorComponent();
            var max = GetMaxColorComponent();

            if (min == max)
            {
                return 0.0;
            }
            else
            {
                var delta = max - min;
                var lum = (max + min) / 510.0;

                if (lum <= 0.5)
                {
                    return delta / (max - min);
                }
                else
                {
                    return delta / (510.0 - max - min);
                }
            }
        }

        private double GetLuminosity()
        {
            var min = GetMinColorComponent();
            var max = GetMaxColorComponent();

            return (max + min) / 510.0;
        }

        private double GetMinColorComponent()
        {
            return Math.Min(Math.Min(R, G), B);
        }

        private double GetMaxColorComponent()
        {
            return Math.Max(Math.Max(R, G), B);
        }

        private double GetColorComponent(double v1, double v2, double vH)
        {
            if (vH < 0)
            {
                vH += 1;
            }

            if (vH > 1)
            {
                vH -= 1;
            }

            if (vH < 1.0 / 6.0)
            {
                return v1 + ((v2 - v1) * 6.0 * vH);
            }
            if (vH < 3.0 / 6.0)
            {
                return v2;
            }
            if (vH < 4.0 / 6.0)
            {
                return v1 + ((v2 - v1) * ((4.0 / 6.0) - vH) * 6.0);
            }

            return v1;
        }

        public static Color FromRGB(byte r, byte g, byte b)
        {
            return new Color(r, g, b);
        }

        public static Color FromHSL(double hue, double saturation, double luminosity)
        {
            return new Color(hue, saturation, luminosity);
        }
    }
}
