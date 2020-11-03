﻿using System;
using System.Threading;
using System.Threading.Tasks;

using Sketch.Shared;
using Sketch.WebApp.Components;

namespace Sketch.WebApp.Components
{
    public interface IGroupManagerModel
    {
        IDisposable OnReceive(Func<UserEvent, Task> handler);
    }
}
