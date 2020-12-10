using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Components;

using Sketch.Shared.Data;
using Sketch.Shared.Models;

namespace Sketch.WebApp.Views
{
    public partial class SKParticipantListItem
    {
        [Parameter]
        public User User { get; set; }
    }
}
