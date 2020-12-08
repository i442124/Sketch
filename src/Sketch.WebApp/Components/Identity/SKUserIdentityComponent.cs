using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Components;

using Sketch.Shared.Data;
using Sketch.Shared.Models;

namespace Sketch.WebApp.Components
{
    public class SKUserIdentityComponent : ComponentBase
    {
        public string Name
        {
            get { return Identity.User.Name; }
        }

        protected Task SetUsernameAsync(string name)
        {
            return Identity.SetUsernameAsync(name);
        }

        [Inject]
        private IUserIdentity Identity { get; set; }
    }
}
