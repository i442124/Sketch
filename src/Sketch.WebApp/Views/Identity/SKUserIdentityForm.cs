using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Components;

using Sketch.Shared.Data;
using Sketch.Shared.Models;
using Sketch.WebApp.Components;

namespace Sketch.WebApp.Views
{
    public partial class SKUserIdentityForm : SKUserIdentityComponent
    {
        private Task OnInputChangedAsync(ChangeEventArgs e)
        {
            return SetUsernameAsync(e.Value.ToString());
        }
    }
}
