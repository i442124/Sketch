using System;

namespace Sketch.Shared
{
    public interface IActionEvent : IAction
    {
        DateTime TimeStamp { get; }
    }
}
