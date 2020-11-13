using System;

namespace Sketch.Shared
{
    public class UndoEvent
    {
        public Undo Undo { get; set; }

        public DateTime TimeStamp { get; set; }
    }
}
