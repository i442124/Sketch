using System.Collections;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

using Sketch;
using Sketch.Shared;

namespace Sketch.WebServer.Storage
{
    public class WhiteboardStorageRecorder : IWhiteboardStorageRecorder
    {
        private readonly ConcurrentStack<IActionEvent> _undoStack
        = new ConcurrentStack<IActionEvent>();

        private readonly ConcurrentStack<IActionEvent> _redoStack
        = new ConcurrentStack<IActionEvent>();

        public IEnumerable Pop()
        {
            if (_undoStack.TryPop(out IActionEvent actionEvent))
            {
                yield return actionEvent;
                _redoStack.Push(actionEvent);

                var actionId = actionEvent.ActionId;
                while (_undoStack.TryPeek(out actionEvent)
                    && actionEvent.ActionId == actionId)
                {
                    if (_undoStack.TryPop(out actionEvent))
                    {
                        yield return actionEvent;
                        _redoStack.Push(actionEvent);
                    }
                }
            }
        }

        public Task<IEnumerable> PopAsync()
        {
            return Task.Run(() => Pop());
        }

        public IEnumerable Restore()
        {
            if (_redoStack.TryPop(out IActionEvent actionEvent))
            {
                yield return actionEvent;
                _undoStack.Push(actionEvent);

                var actionId = actionEvent.ActionId;
                while (_redoStack.TryPeek(out actionEvent))
                {
                    if (actionEvent.ActionId == actionId &&
                        _redoStack.TryPop(out actionEvent))
                    {
                        yield return actionEvent;
                        _undoStack.Push(actionEvent);
                    }
                }
            }
        }

        public Task<IEnumerable> RestoreAsync()
        {
            return Task.Run(() => Restore());
        }

        public void Push(StrokeEvent stroke)
        {
            _redoStack.Clear();
            _undoStack.Push(stroke);
        }

        public Task PushAsync(StrokeEvent stroke)
        {
            return Task.Run(() => Push(stroke));
        }

        public IEnumerable GetStack()
        {
            return _undoStack;
        }

        public Task<IEnumerable> GetStackAsync()
        {
            return Task.Run(() => GetStack());
        }
    }
}
