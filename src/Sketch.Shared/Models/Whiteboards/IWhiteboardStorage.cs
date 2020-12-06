﻿using System.Collections.Generic;
using System.Threading.Tasks;

using Sketch.Shared.Data;
using Sketch.Shared.Data.Ink;

namespace Sketch.Shared.Models
{
    public interface IWhiteboardStorage
    {
        IEnumerable<Action> Actions { get; }

        string Pop();

        Task<string> PopAsync();

        void Push(Stroke stroke);

        Task PushAsync(Stroke stroke);

        void Push(Wipe wipe);

        Task PushAsync(Wipe wipe);

        void Push(Fill fill);

        Task PushAsync(Fill fill);
    }
}
