﻿namespace Sketch.Shared.Data.Ink
{
    public class Wipe : Action
    {
        public WipeStyle Style { get; set; }

        public StylusPointCollection StylusPoints { get; set; }
    }
}
