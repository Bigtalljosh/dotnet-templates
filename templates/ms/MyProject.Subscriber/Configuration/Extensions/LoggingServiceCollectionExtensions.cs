using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MyProject.Subscriber.Configuration.Extensions
{
    internal static class LoggingServiceCollectionExtensions
    {
        public static ILoggingBuilder AddLog4Net(this ILoggingBuilder logging, HostBuilderContext hostContext)
        {
            var isDevelopment = hostContext.Configuration.GetValue<string>("Environment")?.ToLower().Equals("development") ?? false;

            if (isDevelopment)
            {
                logging.AddDebug();
                logging.AddConsole();
            }

            logging.AddConfiguration(hostContext.Configuration.GetSection("Logging"));
            logging.AddApplicationInsights();

            return logging;
        }
    }
}
