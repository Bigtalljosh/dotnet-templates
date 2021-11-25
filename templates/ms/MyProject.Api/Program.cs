using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace MyProject.Api
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.ToString());
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseContentRoot(Directory.GetCurrentDirectory())
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureKestrel(serverOptions =>
                    {
                        serverOptions.AddServerHeader = false;
                    });
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseConfigurationSettings();
                    webBuilder.UseLogging();
                });
    }

    internal static class WebHostBuilderExtensions
    {
        public static IWebHostBuilder UseConfigurationSettings(this IWebHostBuilder webHostBuilder) =>
            webHostBuilder.ConfigureAppConfiguration((hostingContext, config) =>
            {
                var env = hostingContext.HostingEnvironment;

                config.SetBasePath(env.ContentRootPath)
                    .AddJsonFile(Path.Combine("Configuration", "appsettings.json"),
                        optional: true,
                        reloadOnChange: true)
                    .AddJsonFile(Path.Combine("Configuration", $"appsettings.{env.EnvironmentName}.json"),
                        optional: true,
                        reloadOnChange: true)
                    .AddEnvironmentVariables();
            });

        public static IWebHostBuilder UseLogging(this IWebHostBuilder webHostBuilder) =>
            webHostBuilder.ConfigureLogging((hostingContext, logging) =>
            {
                logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));

                if (hostingContext.HostingEnvironment.IsDevelopment())
                {
                    logging.AddConsole();
                    logging.AddDebug();
                }
            });
    }
}
