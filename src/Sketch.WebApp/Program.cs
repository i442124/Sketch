using System;
using System.Net.Http;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Sketch.Shared;
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

            // SKETCH TOOLBOX CONFIG
            builder.Services.AddScoped<IStylusModel, StylusModel>();
            builder.Services.AddScoped<ISizeObjectModel, SizeObjectModel>();
            builder.Services.AddScoped<IColorObjectModel, ColorObjectModel>();

            // SKETCH SERVICES
            builder.Services.AddScoped<IMessageModel, MessageModel>();
            builder.Services.AddScoped<IMessengerModel, MessengerModel>();
            builder.Services.AddScoped<IWhiteboardModel, WhiteboardModel>();
            builder.Services.AddScoped<IWhiteboardStorage, WhiteboardStorage>();

            // SKETCH SUBSCRIPTION SERVICES
            builder.Services.AddScoped<IIdentityModel, IdentityModel>();
            builder.Services.AddScoped<IGroupManagerModel, GroupManagerModel>();
            builder.Services.AddScoped<ISubscriptionModel, SubscriptionModel>();
            builder.Services.AddScoped(typeof(ISubscriptionEventModel<>), typeof(SubscriptionEventModel<>));

            return builder.Build();
        }
    }
}
