namespace Ingestor.Adapters
{
    using System;
    using Microsoft.Extensions.DependencyInjection;
    using Ports;

    public static class DependencyResolution
    {
        public static IServiceCollection AddTvMaze(this IServiceCollection services)
        {
            services.AddTransient<IFetchShowInformation, TvMazeShowProvider>();

            services.AddHttpClient(
                "tvmaze",
                configure =>
                {
                    configure.BaseAddress = new Uri("https://api.tvmaze.com");
                });

            return services;
        }
    }
}
