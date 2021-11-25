using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace MyProject.Subscriber
{
    internal class SubscriberHost : BackgroundService
    {
        private readonly Pat.Subscriber.Subscriber _subscriber;
        private readonly ILogger _logger;
        private CancellationTokenSource _patCancellationToken;
        private readonly IHostApplicationLifetime _applicationLifetime;

        public SubscriberHost(Pat.Subscriber.Subscriber subscriber, ILogger<SubscriberHost> logger, IHostApplicationLifetime applicationLifetime)
        {
            _subscriber = subscriber;
            _logger = logger;
            _applicationLifetime = applicationLifetime;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                _patCancellationToken = new CancellationTokenSource();
                await _subscriber.Run(_patCancellationToken, new[] { Assembly.GetExecutingAssembly() });
            }
            catch (OperationCanceledException)
            {
                _logger.LogWarning("Subscriber process was cancelled");
                Environment.ExitCode = -1;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Subscriber process experienced an unhandled exception");
                Environment.ExitCode = -2;
            }
            finally
            {
                _applicationLifetime.StopApplication();
            }
        }
    }
}
