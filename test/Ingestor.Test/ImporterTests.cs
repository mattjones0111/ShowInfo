namespace Ingestor.Test
{
    using System;
    using System.Linq;
    using System.Net.Http;
    using System.Reflection;
    using System.Threading.Tasks;
    using Configuration;
    using Contracts.V1;
    using Doubles;
    using JustEat.HttpClientInterception;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using NUnit.Framework;
    using Process.Adapters;
    using Process.Ports;

    public class ImporterTests
    {
        [Test]
        public async Task CanImportShowInformation()
        {
            // ARRANGE
            IServiceCollection services = new ServiceCollection();

            services.AddImporterService();

            HttpClientInterceptorOptions options = new HttpClientInterceptorOptions
            {
                ThrowOnMissingRegistration = true
            };

            Assembly thisAssembly = GetType().Assembly;

            HttpRequestInterceptionBuilder _ = new HttpRequestInterceptionBuilder()
                .ForAnyHost().ForHttps().ForGet()
                .Requests().ForPath("shows")
                .Responds().WithContentStream(() => thisAssembly.GetManifestResourceStream("Ingestor.Test.Data.shows.json"))
                .RegisterWith(options)
                .Requests().ForPath("shows/1/cast")
                .Responds().WithContentStream(() => thisAssembly.GetManifestResourceStream("Ingestor.Test.Data.shows-1-cast.json"))
                .RegisterWith(options);

            IHttpClientFactory factory =
                new StubHttpClientFactory(options.CreateHttpClient());

            services.Replace(ServiceDescriptor.Describe(
                typeof(IHttpClientFactory),
                _ => factory,
                ServiceLifetime.Transient));

            InMemoryShowRepository repo = new InMemoryShowRepository();

            services.Replace(
                ServiceDescriptor.Describe(
                    typeof(IShowRepository),
                    _ => repo,
                    ServiceLifetime.Singleton));

            IServiceProvider provider = services.BuildServiceProvider();

            IImporter subject = provider.GetRequiredService<IImporter>();

            // ACT
            await subject.GoAsync();

            // ASSERT
            Show[] fromRepo = await repo.GetAsync(1, 1);

            Show actual = fromRepo.SingleOrDefault();

            Assert.IsNotNull(actual);
            Assert.AreEqual("Under the Dome", actual.Name);
            Assert.AreEqual(1, actual.Id);
            Assert.AreEqual(15, actual.Cast.Length);
        }
    }
}
