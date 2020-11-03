using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Components;

using Sketch.Shared;
using Sketch.WebApp.Components;

namespace Sketch.WebApp.Components
{
    public abstract class SKUserIdentityComponent : ComponentBase
    {
        public string Name
        {
            get { return Identity.User.Name; }
        }

        protected async Task SetUserIdentityAsync(User userIdentity)
        {
            await Identity.SetUserIdentityAsync(userIdentity);
        }

        protected async Task SetUserIdentityNameAsync(string userName)
        {
            await Identity.SetUserIdentityNameAsync(userName);
        }

        [Inject]
        private IIdentityModel Identity { get; set; }
    }
}
