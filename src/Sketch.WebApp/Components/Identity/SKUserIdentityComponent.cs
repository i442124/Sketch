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

        [Parameter]
        public EventCallback<string> NameChanged { get; set; }

        [Parameter]
        public EventCallback<string> NameChanging { get; set; }

        protected async Task SetUsernameAsync(string name)
        {
            await NameChanging.InvokeAsync(name);
            if (!string.IsNullOrEmpty(name))
            {
                await Identity.SetUsernameAsync(name);
                await NameChanged.InvokeAsync(name);
            }
        }

        [Inject]
        private IUserIdentity Identity { get; set; }
    }
}
