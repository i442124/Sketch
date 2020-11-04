using System;

namespace Sketch.Shared
{
    public class SubscribeEvent
    {
        public User User { get; set; }

        public string Channel { get; set; }

        public DateTime DateTime { get; set; }
    }
}
