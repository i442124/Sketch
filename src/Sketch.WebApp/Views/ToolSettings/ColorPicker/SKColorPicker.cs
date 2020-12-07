using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Components;

using Sketch.Shared.Data;
using Sketch.Shared.Data.Ink;
using Sketch.Shared.Data.Ink.Colors;
using Sketch.WebApp.Components;

namespace Sketch.WebApp.Views
{
    public partial class SKColorPicker : SKColorPickerComponent
    {
        [Parameter]
        public int Tints { get; set; }

        [Parameter]
        public int Shades { get; set; }

        [Parameter]
        public IList<Color> Colorants { get; set; }

        private IEnumerable<Color> GetPalette(Color color)
        {
            foreach (var mixture in GetMixturesOfTints(color).Reverse())
            {
                yield return mixture;
            }

            yield return color;

            foreach (var mixture in GetMixturesOfShades(color))
            {
                yield return mixture;
            }
        }

        private IEnumerable<Color> GetMixturesOfTints(Color color)
        {
            return ColorPalette.GetMixtureOfTints(color, Tints);
        }

        private IEnumerable<Color> GetMixturesOfShades(Color color)
        {
            return ColorPalette.GetMixturesOfShades(color, Shades);
        }

        private Task OnColorComponentSelectedAsync(SKColorComponent component)
        {
            return SetValueAsync(component.Color);
        }
    }
}
