using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Sketch.WebServer;
using Sketch.WebServer.Hubs;
using Sketch.WebServer.Services;
using Sketch.WebServer.Storage;

namespace Sketch.WebServer
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Add Blazor Components
            services.AddControllersWithViews();
            services.AddRazorPages();

            // Add SignalR
            services.AddSignalR();
            //services.AddSingleton<IConnectionMultiplexer>(
            //    ConnectionMultiplexer.Connect(Configuration["redis"]));

            // Add WebServer Storage
            services.AddSingleton<IWhiteboardStorage, WhiteboardStorage>();

            // Add WebServer Services
            services.AddSingleton<IMessengerService, MessengerService>();
            services.AddSingleton<IWhiteboardService, WhiteboardService>();
            services.AddSingleton<INotificationService, NotificationService>();

            services.AddSingleton(typeof(IHubConnectionMapper<>), typeof(HubConnectionMapper<>));
            services.AddSingleton(typeof(IHubSubscriptionMapper<>), typeof(HubSubscriptionMapper<>));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapHub<SocialHub>("hub");
                endpoints.MapFallbackToFile("index.html");
            });
        }
    }
}
