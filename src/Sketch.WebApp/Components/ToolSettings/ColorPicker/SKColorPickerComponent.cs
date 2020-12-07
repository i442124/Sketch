using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Components;

using Sketch.Shared.Data;
using Sketch.Shared.Data.Ink;
using Sketch.Shared.Data.Ink.Colors;
using Sketch.Shared.Models;

namespace Sketch.WebApp.Components
{
    public abstract class SKColorPickerComponent : ComponentBase
    {
        public Color Value
        {
            get { return ColorSettings.Color; }
        }

        protected Task SetValueAsync(Color color)
        {
            return ColorSettings.SetColorAsync(color);
        }

        protected Task SetValueAsync(SKColorComponent component)
        {
            return ColorSettings.SetColorAsync(component.Color);
        }

        [Inject]
        private IColorSettings ColorSettings { get; set; }
    }
}
