using System;
using System.Net.Http;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Sketch.Shared;
using Sketch.Shared.Models;
using Sketch.Shared.Services;
using Sketch.WebApp.Components;

namespace Sketch.WebApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await CreateWebAssemblyHost(args).RunAsync();
        }

        public static WebAssemblyHost CreateWebAssemblyHost(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            builder.RootComponents.Add<App>("app");
            builder.Services.AddScoped(service => new HttpClient
            {
                BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
            });

            // SKETCH TOOLS
            builder.Services.AddScoped<IBrushTool, BrushTool>();
            builder.Services.AddScoped<IEraserTool, EraserTool>();
            builder.Services.AddScoped<IPaintBucketTool, PaintBucketTool>();

            // SKETCH TOOL SETTINGS
            builder.Services.AddScoped<IBrushSettings, BrushSettings>();
            builder.Services.AddScoped<IColorSettings, ColorSettings>();
            builder.Services.AddScoped<IStylusSettings, StylusSettings>();

            // SKETCH SERVICES
            builder.Services.AddScoped<INotificationService, NotificationService>();
            builder.Services.AddScoped<ISubscriptionService, SubscriptionService>();

            // SKETCH IDENTITY SERVICES
            builder.Services.AddScoped<IGroupClient, GroupClient>();
            builder.Services.AddScoped<IUserIdentity, UserIdentity>();

            // SKETCH WEBCLIENT SERVICES
            builder.Services.AddScoped<IMessageClient, MessageClient>();
            builder.Services.AddScoped<IWhiteboardClient, WhiteboardClient>();
            builder.Services.AddScoped<IWhiteboardStorage, WhiteboardStorage>();

            return builder.Build();
        }
    }
}
