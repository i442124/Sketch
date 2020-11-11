using System;

namespace Sketch.Shared
{
    public class UnsubscribeEvent
    {
        public User User { get; set; }

        public string Channel { get; set; }

        public DateTime DateTime { get; set; }
    }
}
