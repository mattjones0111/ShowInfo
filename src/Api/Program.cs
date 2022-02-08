namespace Api
{
    using MediatR;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;
    using Middleware;
    using Process;
    using Process.Adapters;
    using Process.Ports;

    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            builder.Services.AddHealthChecks();

            builder.Services.AddMediatR(typeof(IProcessLivesHere));

            builder.Services.AddSingleton<IShowRepository, InMemoryShowRepository>();

            WebApplication app = builder.Build();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapApplicationHealthChecks();
            });

            app.Run();
        }
    }
}
