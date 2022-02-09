namespace Api.Test.Bases
{
    using System;
    using System.Linq;
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Threading.Tasks;
    using Contracts.V1;
    using Helpers;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Process.Adapters;
    using Process.Ports;

    public abstract class ApiTestBase
    {
        readonly Random _rng = new();
        readonly InMemoryShowRepository _repository;
        readonly WebApplicationFactory<Program> _factory;

        protected ApiTestBase()
        {
            _repository = new();

            _factory = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(x =>
                {
                    x.ConfigureServices(services =>
                    {
                        services.Replace(
                            ServiceDescriptor.Describe(
                                typeof(IShowRepository),
                                _ => _repository,
                                ServiceLifetime.Singleton));
                    });
                });
        }

        protected async Task AddTestDataAsync(int count)
        {
            Show[] shows = Enumerable
                .Range(1, count)
                .Select(x => new Show
                {
                    Id = x,
                    Name = $"Show {x}",
                    Cast = GetRandomCast(x)
                })
                .ToArray();

            foreach (Show show in shows)
            {
                await _repository.StoreAsync(show);
            }
        }

        protected HttpClient GetClient() => _factory.CreateClient();

        protected async Task<Show[]> GetShowsAsync(int pageNumber, int pageSize)
        {
            HttpClient client = GetClient();

            HttpResponseMessage response =
                await client.GetAsync($"/shows?pageNumber={pageNumber}&pageSize={pageSize}");

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<Show[]>();
        }

        CastMember[] GetRandomCast(int showId)
        {
            int number = _rng.Next(1, 5);
            int baseId = showId * 10000;

            return Enumerable
                .Range(1, number)
                .Select(x => new CastMember
                {
                    Id = baseId + x,
                    Name = "Cast Member {baseId + x}",
                    Birthday = Any.DateTime(1930, 2010)
                })
                .ToArray();
        }
    }
}
