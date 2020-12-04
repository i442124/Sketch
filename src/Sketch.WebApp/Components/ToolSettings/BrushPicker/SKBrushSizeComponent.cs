using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Components;

using Sketch.Shared.Data;
using Sketch.Shared.Models;

namespace Sketch.WebApp.Components
{
    public abstract class SKBrushSizeComponent : ComponentBase
    {
        public float Value
        {
            get { return BrushSettings.Size; }
        }

        protected Task SetValueAsync(float newSize)
        {
            return BrushSettings.SetSizeAsync(newSize);
        }

        [Inject]
        private IBrushSettings BrushSettings { get; set; }
    }
}
