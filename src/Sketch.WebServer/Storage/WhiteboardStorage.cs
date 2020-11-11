using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Sketch.Shared;
using Sketch.WebServer;
using Sketch.WebServer.Services;

namespace Sketch.WebServer.Storage
{
    public class WhiteboardStorage : IWhiteboardStorage
    {
        private readonly ConcurrentDictionary<string, IWhiteboardStorageRecorder> _recordings
        = new ConcurrentDictionary<string, IWhiteboardStorageRecorder>();

        public WhiteboardStorage()
        {
        }

        public IEnumerable Pop(string channel)
        {
            return _recordings[channel].Pop();
        }

        public Task<IEnumerable> PopAsync(string channel)
        {
            return _recordings[channel].PopAsync();
        }

        public void Push(string channel, StrokeEvent stroke)
        {
            var recording = _recordings.GetOrAdd(channel, new WhiteboardStorageRecorder());
            recording.Push(stroke);
        }

        public Task PushAsync(string channel, StrokeEvent stroke)
        {
            var recording = _recordings.GetOrAdd(channel, new WhiteboardStorageRecorder());
            return recording.PushAsync(stroke);
        }

        public IEnumerable GetStack(string channel)
        {
            return _recordings[channel].GetStack();
        }
    }
}
