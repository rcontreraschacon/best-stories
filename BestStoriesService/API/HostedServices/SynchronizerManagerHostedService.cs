using System;
using System.Threading;
using System.Threading.Tasks;
using Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace API.HostedServices
{
    public class SynchronizerManagerHostedService : IHostedService
    {
        private readonly ILogger _logger;
        private readonly TimeSpan _timeBetweenSync;
        public IServiceProvider Services { get; }

        public SynchronizerManagerHostedService(IServiceProvider services,
            ILogger<SynchronizerManagerHostedService> logger, IConfiguration configuration)
        {
            Services = services;
            _logger = logger;
            _timeBetweenSync = configuration.GetValue<TimeSpan>("SynchronizerManager:TimeBetweenSync");
        }


        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation(
                "Consume Scoped Service Hosted Service is starting.");

            await DoWork(cancellationToken);
        }

        private async Task DoWork(CancellationToken cancellationToken)
        {
            _logger.LogInformation(
                "Consume Scoped Service Hosted Service is working.");

            using (var scope = Services.CreateScope())
            {
                var synchronizer =
                    scope.ServiceProvider
                        .GetRequiredService<ISynchronizer>();
                do
                {
                    await synchronizer.SynchronizeAsync(cancellationToken);
                    await Task.Delay(_timeBetweenSync, cancellationToken);
                } while (!cancellationToken.IsCancellationRequested);

            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation(
                "Consume Scoped Service Hosted Service is stopping.");

            return Task.CompletedTask;
        }
    }
}