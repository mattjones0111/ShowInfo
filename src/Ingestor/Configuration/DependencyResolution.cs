namespace Ingestor.Configuration
{
    using Adapters;
    using Microsoft.Extensions.DependencyInjection;
    using Process.Adapters;
    using Process.Configuration;
    using Process.Ports;

    public static class DependencyResolution
    {
        public static IServiceCollection AddImporterService(
            this IServiceCollection services)
        {
            services.AddFeatures();
            services.AddSingleton<IShowRepository, InMemoryShowRepository>();
            services.AddHostedService<Worker>();
            services.AddTransient<IImporter, DefaultImporter>();
            services.AddTvMaze();

            return services;
        }
    }
}
