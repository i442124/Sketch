using System.Threading;
using System.Threading.Tasks;

using Sketch.Shared;

namespace Sketch.WebApp.Components
{
    public interface IIdentityModel
    {
        User User { get; }

        Task SetUserIdentityAsync(User user);

        Task SetUserIdentityNameAsync(string name);
    }
}
