using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

using Sketch.Shared;
using Sketch.WebApp.Components;

namespace Sketch.WebApp.Components
{
    public class WhiteboardStorage : IWhiteboardStorage
    {
        private readonly Stack<WhiteboardAction> _undoStack
        = new Stack<WhiteboardAction>(capacity: 1024);

        private readonly Stack<WhiteboardAction> _redoStack
        = new Stack<WhiteboardAction>(capacity: 1024);

        public IEnumerable<WhiteboardAction> Pop()
        {
            IEnumerable<WhiteboardAction> _()
            {
                if (_undoStack.TryPop(out WhiteboardAction action))
                {
                    yield return action;
                    _redoStack.Push(action);

                    var actionId = action.ActionId;
                    while (_undoStack.TryPeek(out action) && action.ActionId == actionId)
                    {
                        if (_undoStack.TryPop(out action))
                        {
                            yield return action;
                            _redoStack.Push(action);
                        }
                    }
                }
            }

            return _().ToList().AsReadOnly();
        }

        public void Push(Stroke stroke, Func<Stroke, Task> strokeAction)
        {
            _redoStack.Clear();
            _undoStack.Push(new WhiteboardAction(stroke, strokeAction));
        }

        public void Push(Wipe wipe, Func<Wipe, Task> wipeAction)
        {
            _redoStack.Clear();
            _undoStack.Push(new WhiteboardAction(wipe, wipeAction));
        }

        public void Push(Fill fill, Func<Fill, Task> fillAction)
        {
            _redoStack.Clear();
            _undoStack.Push(new WhiteboardAction(fill, fillAction));
        }

        public void Push(Clear clear, Func<Clear, Task> clearAction)
        {
            _redoStack.Clear();
            _undoStack.Push(new WhiteboardAction(clear, clearAction));
        }

        public IEnumerator<WhiteboardAction> GetEnumerator()
        {
            return _undoStack.Reverse().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _undoStack.Reverse().GetEnumerator();
        }
    }
}
