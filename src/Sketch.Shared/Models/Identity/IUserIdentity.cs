using System;
using System.Threading;
using System.Threading.Tasks;

using Sketch.Shared.Data;
using Sketch.Shared.Services;

namespace Sketch.Shared.Models
{
    public interface IUserIdentity
    {
        User User { get; }

        Task SetUsernameAsync(string name);
    }
}
