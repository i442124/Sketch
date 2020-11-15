using System;
using System.Net.Http;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Sketch.WebApp;
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

            // SKETCH TOOLBOX
            builder.Services.AddScoped<IBrushModel, BrushModel>();
            builder.Services.AddScoped<IBucketModel, BucketModel>();
            builder.Services.AddScoped<IEraserModel, EraserModel>();
            builder.Services.AddScoped<IPipetteModel, PipetteModel>();

            // SKETCH TOOLBOX CONFIGURATION
            builder.Services.AddScoped<IStylusModel, StylusModel>();
            builder.Services.AddScoped<IStylusTipModel, StylusTipModel>();

            // SKETCH SERVICES
            builder.Services.AddScoped<IMessageModel, MessageModel>();
            builder.Services.AddScoped<IMessengerModel, MessengerModel>();
            builder.Services.AddScoped<IWhiteboardModel, WhiteboardModel>();
            builder.Services.AddTransient<IWhiteboardStorage, WhiteboardStorage>();

            // SKETCH SUBSCRIPTIONS
            builder.Services.AddScoped<IIdentityModel, IdentityModel>();
            builder.Services.AddScoped<IGroupManagerModel, GroupManagerModel>();
            builder.Services.AddScoped<ISubscriptionModel, SubscriptionModel>();

            return builder.Build();
        }
    }
}
