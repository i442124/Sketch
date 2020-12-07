using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Components;

using Sketch.Shared.Data;
using Sketch.Shared.Models;

namespace Sketch.WebApp.Components
{
    public abstract class SKBrushSettingsComponent : ComponentBase
    {
        public float Size
        {
            get { return BrushSettings.Size; }
        }

        public float Opacity
        {
            get { return BrushSettings.Opacity; }
        }

        protected void SetBrushSize(float size)
        {
            BrushSettings.SetSize(size);
        }

        protected Task SetBrushSizeAsync(float size)
        {
            return BrushSettings.SetSizeAsync(size);
        }

        protected void SetBrushOpacity(float opacity)
        {
            BrushSettings.SetOpacity(opacity);
        }

        protected Task SetBrushOpacityAsync(float opacity)
        {
            return BrushSettings.SetOpacityAsync(opacity);
        }

        [Inject]
        private IBrushSettings BrushSettings { get; set; }
    }
}
