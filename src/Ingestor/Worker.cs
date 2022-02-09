namespace Ingestor
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Hosting;

    public class Worker : BackgroundService
    {
        readonly IImporter _importer;

        public Worker(IImporter importer)
        {
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
