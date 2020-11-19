using System;

namespace Sketch.Shared
{
    public class UnsubscribeEvent
    {
        public DateTime DateTime { get; set; }

        public Unsubscribe Unsubscription { get; set; }
    }
}
