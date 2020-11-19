using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Components;

namespace Sketch.WebApp.Components
{
    public abstract class SKSizePickerComponent : ComponentBase
    {
        public float Value
        {
            get { return ObjectSizeModel.Size; }
        }

        protected async Task SetValueAsync(float newSize)
        {
            await ObjectSizeModel.SetSizeAsync(newSize);
        }

        [Inject]
        private ISizeObjectModel ObjectSizeModel { get; set; }
    }
}
