namespace Ingestor
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Contracts.V1;
    using MediatR;
    using Microsoft.Extensions.Logging;
    using Ports;
    using Process.Features.Shows;

    public interface IImporter
    {
        Task GoAsync();
    }

    public sealed class DefaultImporter : IImporter
    {
        readonly IFetchShowInformation _sourceProvider;
        readonly IMediator _mediator;
        readonly ILogger _logger;

        public DefaultImporter(
            IFetchShowInformation sourceProvider,
            IMediator mediator,
            ILogger<DefaultImporter> logger)
        {
            _sourceProvider = sourceProvider;
            _mediator = mediator;
            _logger = logger;
        }

        public async Task GoAsync()
        {
            ShowInfo[] shows;

            try
            {
                shows = await _sourceProvider.GetShowsAsync();
            }
            catch (ShowProviderException ex)
            {
                _logger.LogWarning(ex, "Error fetching shows.");
                return;
            }

            Task[] tasks = shows
                .Select(MapAndUploadShow)
                .ToArray();

            await Task.WhenAll(tasks);
        }

        async Task MapAndUploadShow(ShowInfo show)
        {
            CastInfo[] cast;

            try
            {
                cast = await _sourceProvider.GetCastForShowAsync(show.Id);
            }
            catch (ShowProviderException ex)
            {
                _logger.LogWarning(ex, "Error fetching cast information.");
                return;
            }

            Show showContract = new Show
            {
                Id = show.Id,
                Name = show.Name,
                Cast = cast.Select(x => new CastMember
                {
                    Id = x.Id,
                    Name = x.Name,
                    Birthday = x.Birthday
                }).ToArray(),
            };

            try
            {
                await _mediator.Send(
                    new Put.Command
                    {
                        Show = showContract
                    });
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Error storing Show info.");
            }
        }
    }
}
