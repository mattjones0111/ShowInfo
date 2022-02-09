namespace Ingestor
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;

    public class Worker : BackgroundService
    {
        readonly IImporter _importer;
        readonly ILogger _logger;

        public Worker(ILogger<Worker> logger, IImporter importer)
        {
            _logger = logger;
            _importer = importer;
        }

        protected override async Task ExecuteAsync(
            CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await _importer.GoAsync();

                await Task.Delay(TimeSpan.FromDays(1), stoppingToken);
            }
        }
    }
}
