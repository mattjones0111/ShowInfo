namespace Ingestor.Adapters
{
    using System;
    using System.Linq;
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;
    using Ports;

    public sealed class TvMazeShowProvider : IFetchShowInformation
    {
        readonly HttpClient _httpClient;
        readonly ILogger _logger;

        public TvMazeShowProvider(
            IHttpClientFactory httpClientFactory,
            ILogger<TvMazeShowProvider> logger)
        {
            _logger = logger;

            _httpClient = httpClientFactory.CreateClient("tvmaze");
        }

        public async Task<ShowInfo[]> GetShowsAsync()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync("/shows");

                response.EnsureSuccessStatusCode();

                return await response.Content.ReadFromJsonAsync<ShowInfo[]>();
            }
            catch (HttpRequestException ex)
            {
                _logger.LogWarning(ex, "Could not get show info.");

                throw new ShowProviderException(ex);
            }
        }

        public async Task<CastInfo[]> GetCastForShowAsync(int showId)
        {
            try
            {
                HttpResponseMessage response =
                    await _httpClient.GetAsync($"/shows/{showId}/cast");

                response.EnsureSuccessStatusCode();

                TvMazeCastInfo[] cast =
                    await response.Content.ReadFromJsonAsync<TvMazeCastInfo[]>();

                return cast
                    .Select(x => new CastInfo
                    {
                        Id = x.Person.Id,
                        Name = x.Person.Name,
                        Birthday = x.Person.Birthday
                    })
                    .ToArray();
            }
            catch (HttpRequestException ex)
            {
                _logger.LogWarning(ex, "Could not get show info.");

                throw new ShowProviderException(ex);
            }
        }

        class TvMazeCastInfo
        {
            public TvMazePersonInfo Person { get; set; }
        }

        class TvMazePersonInfo
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public DateTime? Birthday { get; set; }
        }
    }
}
