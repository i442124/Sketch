﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

using Sketch.Shared.Data.Ink;
using Sketch.Shared.Services;

namespace Sketch.Shared.Models
{
    public class WhiteboardStorage : IWhiteboardStorage
    {
        public IEnumerable<Data.Action> Actions => throw new NotImplementedException();

        public string Pop()
        {
            throw new NotImplementedException();
        }

        public Task<string> PopAsync()
        {
            throw new NotImplementedException();
        }

        public void Push(Stroke stroke)
        {
            throw new NotImplementedException();
        }

        public Task PushAsync(Stroke stroke)
        {
            throw new NotImplementedException();
        }

        public void Push(Wipe wipe)
        {
            throw new NotImplementedException();
        }

        public Task PushAsync(Wipe wipe)
        {
            throw new NotImplementedException();
        }

        public void Push(Fill fill)
        {
            throw new NotImplementedException();
        }

        public Task PushAsync(Fill fill)
        {
            throw new NotImplementedException();
        }
    }
}
