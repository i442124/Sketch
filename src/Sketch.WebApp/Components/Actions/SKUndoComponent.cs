using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Components;

using Sketch.Shared.Data;
using Sketch.Shared.Models;

namespace Sketch.WebApp.Components
{
    public abstract class SKUndoComponent : ComponentBase
    {
        protected Task UndoAsync()
        {
            return UndoAsync(new Undo { });
        }

        protected Task UndoAsync(Undo undo)
        {
            return Whiteboard.UndoAsync(undo);
        }

        [Inject]
        private IWhiteboardClient Whiteboard { get; set; }
    }
}
