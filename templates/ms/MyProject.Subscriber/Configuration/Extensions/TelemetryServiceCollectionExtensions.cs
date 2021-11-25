using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pat.Subscriber.Telemetry.StatsD;

namespace MyProject.Subscriber.Configuration.Extensions
{
    internal static class TelemetryServiceCollectionExtensions
    {
        public static IServiceCollection AddTelemetry(this IServiceCollection services, IConfiguration configuration)
        {
            var statsDConfig = configuration.GetSection("StatsD").Get<StatisticsReporterConfiguration>();

            services.AddApplicationInsightsTelemetryWorkerService();
            services.AddSingleton(statsDConfig);
            services.AddSingleton<IStatisticsReporter, StatisticsReporter>();
            return services;
        }
    }
}
