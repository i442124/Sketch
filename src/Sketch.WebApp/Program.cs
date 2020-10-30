using System;
using System.Net.Http;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Sketch.WebApp.Areas.Configuration;
using Sketch.WebApp.Areas.Messages;
using Sketch.WebApp.Areas.Subscriptions;
using Sketch.WebApp.Areas.Toolbox;
using Sketch.WebApp.Areas.Tools;
using Sketch.WebApp.Areas.Whiteboard;

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

            // SKETCH TOOLBOX
            builder.Services.AddScoped<IBrushModel, BrushModel>();
            builder.Services.AddScoped<IBucketModel, BucketModel>();
            builder.Services.AddScoped<IEraserModel, EraserModel>();
            builder.Services.AddScoped<IPipetteModel, PipetteModel>();

            // SKETCH TOOLBOX CONFIGURATION
            builder.Services.AddScoped<IStylusModel, StylusModel>();
            builder.Services.AddScoped<IStylusTipModel, StylusTipModel>();

            // SKETCH SERVICES
            builder.Services.AddScoped<IMessengerModel, MessengerModel>();
            builder.Services.AddScoped<IWhiteboardModel, WhiteboardModel>();
            builder.Services.AddScoped<ISubscriptionModel, SubscriptionModel>();

            return builder.Build();
        }
    }
}
