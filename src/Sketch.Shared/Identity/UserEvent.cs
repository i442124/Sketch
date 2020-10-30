using System;

namespace Sketch.Shared
{
    public class UserEvent
    {
        public User User { get; set; }

        public bool Connected { get; set; }

        public DateTime TimeStamp { get; set; }
    }
}
