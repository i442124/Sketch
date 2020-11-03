using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Components;

namespace Sketch.WebApp.Components
{
    public abstract class SKSizeComponent : ComponentBase
    {
        public float Value
        {
            get { return Stylus.Size; }
        }

        protected Task SetStylusSizeAsync(float size)
        {
            return Stylus.SetSizeAsync(size);
        }

        [Inject]
        private IStylusTipModel Stylus { get; set; }
    }
}
