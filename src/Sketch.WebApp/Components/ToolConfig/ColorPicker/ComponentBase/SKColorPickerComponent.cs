using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Components;

using Sketch.Shared;
using Sketch.WebApp.Components;

namespace Sketch.WebApp.Components
{
    public abstract class SKColorPickerComponent : ComponentBase
    {
        public Color Value
        {
            get { return ColorObject.Color; }
        }

        protected async Task SetValueAsync(Color color)
        {
            await ColorObject.SetColorAsync(color);
        }

        [Inject]
        private IColorObjectModel ColorObject { get; set; }
    }
}
