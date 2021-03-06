namespace Ingestor
{
    using System.Threading.Tasks;
    using Configuration;
    using Microsoft.Extensions.Hosting;

    public class Program
    {
        public static async Task Main(string[] args)
        {
            IHost host = Host.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    services.AddImporterService();
                })
                .Build();

            await host.RunAsync();
        }
    }
}
