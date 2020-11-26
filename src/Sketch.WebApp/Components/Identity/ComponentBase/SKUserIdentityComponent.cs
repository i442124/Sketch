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

        protected async Task SetNameAsync(string userName)
        {
            await Identity.SetUserNameAsync(userName);
        }

        protected async Task SetIdentityAsync(User userIdentity)
        {
            await Identity.SetUserIdentityAsync(userIdentity);
        }

        [Inject]
        private IIdentityModel Identity { get; set; }
    }
}
