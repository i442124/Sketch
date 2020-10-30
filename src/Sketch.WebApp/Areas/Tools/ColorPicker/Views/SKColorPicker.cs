using System;
using System.Threading;
using System.Threading.Tasks;

namespace Sketch.WebApp.Areas.Tools
{
    public partial class SKColorPicker : SKColorPickerComponent
    {
        private Task OnColorSelectedAsync(SKColorComponent component)
        {
            return SetPipetteColorAsync(component.Color);
        }
    }
}
