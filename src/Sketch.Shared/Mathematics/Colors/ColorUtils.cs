using System;

namespace Sketch.Shared
{
    internal static class ColorUtils
    {
        public static double GetHue(Color color)
        {
            return GetHue(color.R, color.G, color.B);
        }

        public static double GetHue(byte r, byte g, byte b)
        {
            if (r == g && g == b)
            {
                return 0.0;
            }

            var min = GetMinColorComponent(r, g, b);
            var max = GetMaxColorComponent(r, g, b);

            var hue = 0.0;
            var delta = max - min;

            if (r == max)
            {
                hue = (g - b) / delta;
            }
            else if (g == max)
            {
                hue = 2 + ((b - r) / delta);
            }
            else if (b == max)
            {
                hue = 4 + ((r - g) / delta);
            }

            hue *= 60.0;
            hue = hue < 0.0 ? hue + 360.0 : hue;
            return hue;
        }

        public static double GetSaturation(Color color)
        {
            return GetSaturation(color.R, color.G, color.B);
        }

        public static double GetSaturation(byte r, byte g, byte b)
        {
            var min = GetMinColorComponent(r, g, b);
            var max = GetMaxColorComponent(r, g, b);

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
                    return delta / (max + min);
                }
                else
                {
                    return delta / (510.0 - max - min);
                }
            }
        }

        public static double GetLuminosity(Color color)
        {
            return GetLuminosity(color.R, color.G, color.B);
        }

        public static double GetLuminosity(byte r, byte g, byte b)
        {
            var min = GetMinColorComponent(r, g, b);
            var max = GetMaxColorComponent(r, g, b);

            return (max + min) / 510.0;
        }

        public static double GetMinColorComponent(Color color)
        {
            return GetMinColorComponent(color.R, color.G, color.B);
        }

        public static double GetMinColorComponent(byte r, byte g, byte b)
        {
            return Math.Min(Math.Min(r, g), b);
        }

        public static double GetMaxColorComponent(Color color)
        {
            return GetMaxColorComponent(color.R, color.G, color.B);
        }

        public static double GetMaxColorComponent(byte r, byte g, byte b)
        {
            return Math.Max(Math.Max(r, g), b);
        }

        public static Color FromRGB(byte r, byte g, byte b)
        {
            return new Color { R = r, G = g, B = b };
        }

        public static Color FromHLS(double hue, double saturation, double luminosity)
        {
            if (saturation == 0)
            {
                var value = GetRoundedColorComponent(luminosity * 255.0);
                return FromRGB(value, value, value);
            }
            else
            {
                double v1, v2;
                v2 = (luminosity < 0.5)
                    ? (luminosity * (1 + saturation))
                    : ((luminosity + saturation) - (luminosity * saturation));

                v1 = (2 * luminosity) - v2;
                hue /= 360.0;

                var r = GetRoundedColorComponent(255.0 * GetHueColorComponent(v1, v2, hue + (1.0 / 3)));
                var g = GetRoundedColorComponent(255.0 * GetHueColorComponent(v1, v2, hue));
                var b = GetRoundedColorComponent(255.0 * GetHueColorComponent(v1, v2, hue - (1.0 / 3)));
                return FromRGB(r, g, b);
            }
        }

        public static Color FromHex(string hex)
        {
            return default;
        }

        private static double GetHueColorComponent(double v1, double v2, double vH)
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

        private static byte GetRoundedColorComponent(double value)
        {
            return (byte)Math.Round(value, MidpointRounding.AwayFromZero);
        }
    }
}
