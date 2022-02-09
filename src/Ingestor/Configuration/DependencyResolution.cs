namespace Ingestor.Configuration
{
    using Adapters;
    using MediatR;
    using Microsoft.Extensions.DependencyInjection;
    using Process;
    using Process.Adapters;
    using Process.Ports;

    public static class DependencyResolution
    {
        public static IServiceCollection AddImporterService(
            this IServiceCollection services)
        {
            services.AddMediatR(typeof(IProcessLivesHere));
            services.AddSingleton<IShowRepository, InMemoryShowRepository>();
            services.AddHostedService<Worker>();
            services.AddTransient<IImporter, DefaultImporter>();
            services.AddTvMaze();

            return services;
        }
    }
}
