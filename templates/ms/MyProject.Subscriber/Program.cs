using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pat.Subscriber.NetCoreDependencyResolution;
using MyProject.Business.House;
using MyProject.Subscriber.Configuration.Extensions;
using MyProject.Subscriber.Handlers;
using System.IO;
using System.Threading.Tasks;

namespace MyProject.Subscriber
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var host = new HostBuilder()
            .ConfigureHostConfiguration(configHost =>
            {
                configHost.SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile(Path.Combine("Configuration", "hostsettings.json"),
                        optional: false,
                        reloadOnChange: true)
                    .AddEnvironmentVariables();
            })
            .ConfigureAppConfiguration((hostContext, configApp) =>
            {
                configApp
                    .AddJsonFile(Path.Combine("Configuration", "appsettings.json"),
                        optional: false,
                        reloadOnChange: true)
                    .AddEnvironmentVariables();
            })
            .ConfigureServices((hostContext, services) =>
            {
                services
                    .AddTelemetry(hostContext.Configuration)
                    .AddBasePatLiteServices(hostContext.Configuration)
                    .AddHandlersFromAssemblyContainingType<CreateHouseCommandV1Handler>()
                    .AddHostedService<SubscriberHost>();

                services.AddTransient<IHouseService, HouseService>();
            })
            .UseConsoleLifetime()
            .Build();

            await host.RunAsync();
        }
    }
}
