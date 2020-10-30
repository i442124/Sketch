﻿using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Sketch.WebApp.Areas.Tools
{
    public partial class SKBrushButton : SKBrushComponent
    {
        private Task OnClickAsync(MouseEventArgs e)
        {
            return UseBrushAsync();
        }
    }
}