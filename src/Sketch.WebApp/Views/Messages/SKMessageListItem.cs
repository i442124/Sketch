using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Components;

using Sketch.Shared.Data;
using Sketch.Shared.Models;

namespace Sketch.WebApp.Views
{
    public partial class SKMessageListItem
    {
        [Parameter]
        public Message Message { get; set; }
    }
}
