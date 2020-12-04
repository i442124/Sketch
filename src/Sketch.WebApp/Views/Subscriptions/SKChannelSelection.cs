using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Components;

using Sketch.Shared.Data;
using Sketch.WebApp.Components;

namespace Sketch.WebApp.Views
{
    public partial class SKChannelSelection : SKChannelComponent
    {
        public string Channel { get; private set; }

        private SemaphoreSlim Semaphore { get; set; }
        = new SemaphoreSlim(initialCount: 1, maxCount: 1);

        private async Task OnSelectionChangedAsync(ChangeEventArgs e)
        {
            await Semaphore.WaitAsync();

            try
            {
                if (!string.IsNullOrEmpty(Channel))
                {
                    await UnsubscribeAsync(Channel);
                }

                await SubscribeAsync(Channel = (string)e.Value);
            }
            finally
            {
                Semaphore.Release();
            }
        }
    }
}
