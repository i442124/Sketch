﻿namespace Sketch.Shared.Data
{
    public class StylusPoint
    {
        public int X { get; set; }

        public int Y { get; set; }

        public static implicit operator StylusPoint((int X, int Y) point)
        {
            return new StylusPoint { X = point.X, Y = point.Y };
        }
    }
}
