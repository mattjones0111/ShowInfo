namespace Ingestor.Adapters
{
    using System;
    using System.Net.Http;
    using Microsoft.Extensions.DependencyInjection;
    using Polly;
    using Polly.Extensions.Http;
    using Ports;

    public static class DependencyResolution
    {
        public static IServiceCollection AddTvMaze(this IServiceCollection services)
        {
            services.AddTransient<IFetchShowInformation, TvMazeShowProvider>();

            services
                .AddHttpClient(
                    "tvmaze",
                    configure =>
                    {
                        configure.BaseAddress = new Uri("https://api.tvmaze.com");
                    })
                .AddPolicyHandler(GetRetryPolicy());

            return services;
        }

        static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
                .WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
        }
    }
}
