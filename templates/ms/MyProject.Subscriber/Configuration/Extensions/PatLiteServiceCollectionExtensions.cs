using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pat.Sender;
using Pat.Sender.Correlation;
using Pat.Sender.MessageGeneration;
using Pat.Sender.NetCoreLog;
using Pat.Subscriber;
using Pat.Subscriber.BatchProcessing;
using Pat.Subscriber.MessageProcessing;
using Pat.Subscriber.NetCoreDependencyResolution;
using Pat.Subscriber.Telemetry.StatsD;
using System;

namespace MyProject.Subscriber.Configuration.Extensions
{
    public static class PatLiteServiceCollectionExtensions
    {
        public static IServiceCollection AddBasePatLiteServices(this IServiceCollection services, IConfiguration configuration)
        {
            var senderSettings = new PatSenderSettings();
            configuration.GetSection("PatLite:Sender").Bind(senderSettings);

            var subscriberSettings = new SubscriberConfiguration();
            configuration.GetSection("PatLite:Subscriber").Bind(subscriberSettings);

            var patOptions = new PatLiteOptionsBuilder(subscriberSettings)
              .DefineMessagePipeline
                .With<MonitoringMessageProcessingBehaviour>()
                .With<DefaultMessageProcessingBehaviour>()
                .With<InvokeHandlerBehaviour>()
              .DefineBatchPipeline
                .With<MonitoringBatchProcessingBehaviour>()
                .With<DefaultBatchProcessingBehaviour>()
              .Build();

            services
                .AddSingleton(senderSettings)
                .AddSingleton(subscriberSettings)
                .AddTransient<IMessagePublisher, MessagePublisher>()
                .AddTransient<IMessageSender, MessageSender>()
                .AddTransient<IMessageGenerator, MessageGenerator>()
                .AddTransient(s => new MessageProperties(new LiteralCorrelationIdProvider($"{Guid.NewGuid()}")))
                .AddPatLite(patOptions)
                .AddLogging()
                .AddPatSenderNetCoreLogAdapter();

            return services;
        }
    }
}
