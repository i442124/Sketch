using System;

namespace Sketch.Shared
{
    public class StrokeEvent : IActionEvent
    {
        public Stroke Stroke { get; set; }

        public DateTime TimeStamp { get; set; }

        public string ActionId => Stroke.ActionId;
    }
}
