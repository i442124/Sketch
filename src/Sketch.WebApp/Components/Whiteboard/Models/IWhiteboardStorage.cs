using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;

using Sketch;
using Sketch.Shared;

namespace Sketch.WebApp.Components
{
    public interface IWhiteboardStorage : IEnumerable<WhiteboardAction>
    {
        IEnumerable<WhiteboardAction> Pop();

        void Push(Stroke stroke, Func<Stroke, Task> strokeAction);

        void Push(Wipe wipe, Func<Wipe, Task> wipeAction);

        void Push(Fill wipe, Func<Fill, Task> fillAction);

        void Push(Clear clear, Func<Clear, Task> clearAsync);
    }
}
