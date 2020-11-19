using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Sketch.WebApp.Components
{
    public partial class SKBucketButton : SKBucketComponent
    {
        private Task OnClickAsync(MouseEventArgs e)
        {
            return UseAsync();
        }
    }
}
