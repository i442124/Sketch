using System;
using System.Threading;
using System.Threading.Tasks;

using Sketch.Shared;
using Sketch.WebApp.Components;

namespace Sketch.WebApp.Components
{
    public class WhiteboardAction
    {
        private readonly Drawable _drawable;
        private readonly Func<Task> _drawableAction;

        public string ActionId => _drawable.ActionId;

        public WhiteboardAction(Stroke stroke, Func<Stroke, Task> strokeAction)
        {
            _drawable = stroke;
            _drawableAction = new Func<Task>(() => strokeAction(stroke));
        }

        public WhiteboardAction(Wipe wipe, Func<Wipe, Task> wipeAction)
        {
            _drawable = wipe;
            _drawableAction = new Func<Task>(() => wipeAction(wipe));
        }

        public WhiteboardAction(Fill fill, Func<Fill, Task> fillAction)
        {
            _drawable = fill;
            _drawableAction = new Func<Task>(() => fillAction(fill));
        }

        public WhiteboardAction(Clear clear, Func<Clear, Task> clearAction)
        {
            _drawable = clear;
            _drawableAction = new Func<Task>(() => clearAction(clear));
        }

        public async Task DrawAsync() => await _drawableAction.Invoke();
    }
}
