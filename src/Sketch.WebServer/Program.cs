using System;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Sketch.WebServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            var port = Environment.GetEnvironmentVariable("PORT");

            var hostBuilder = Host.CreateDefaultBuilder(args);
            hostBuilder.ConfigureWebHostDefaults(webBuilder =>
            {
                if (!string.IsNullOrEmpty(port))
                {
                    webBuilder
                        .UseStartup<Startup>()
                        .UseUrls("http://*:" + port);
                }
                else
                {
                    webBuilder
                        .UseStartup<Startup>();
                }
            });

            return hostBuilder;
        }
    }
}
