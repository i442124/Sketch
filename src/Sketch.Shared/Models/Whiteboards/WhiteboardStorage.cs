using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Sketch.Shared.Data;
using Sketch.Shared.Data.Ink;

namespace Sketch.Shared.Models
{
    public class WhiteboardStorage : IWhiteboardStorage
    {
        private readonly Stack<Action> _undoStack = new Stack<Action>();
        private readonly Stack<Action> _redoStack = new Stack<Action>();

        public IEnumerable<Action> Actions
        {
            get { return _undoStack.Reverse(); }
        }

        public string Pop()
        {
            if (_undoStack.TryPop(out Action action))
            {
                _redoStack.Push(action);
                var actionId = action.ActionId;

                while (_undoStack.TryPeek(out action) && action.ActionId == actionId)
                {
                    if (_undoStack.TryPop(out action))
                    {
                        _redoStack.Push(action);
                    }
                }

                return actionId;
            }

            return null;
        }

        public Task<string> PopAsync()
        {
            return Task.FromResult(Pop());
        }

        public void Push(Stroke stroke)
        {
            _redoStack.Clear();
            _undoStack.Push(stroke);
        }

        public Task PushAsync(Stroke stroke)
        {
            return Task.Run(() => Push(stroke));
        }

        public void Push(Wipe wipe)
        {
            _redoStack.Clear();
            _undoStack.Push(wipe);
        }

        public Task PushAsync(Wipe wipe)
        {
            return Task.Run(() => Push(wipe));
        }

        public void Push(Fill fill)
        {
            _redoStack.Clear();
            _undoStack.Push(fill);
        }

        public Task PushAsync(Fill fill)
        {
            return Task.Run(() => Push(fill));
        }

        public void Push(Clear clear)
        {
            _redoStack.Clear();
            _undoStack.Push(clear);
        }

        public Task PushAsync(Clear clear)
        {
            return Task.Run(() => Push(clear));
        }
    }
}
