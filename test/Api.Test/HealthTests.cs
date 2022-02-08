using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;

namespace Api.Test
{
    public class HealthTests
    {
        [Test]
        public async Task ApplicationIsHealthy()
        {
            WebApplicationFactory<Program> factory =
                new WebApplicationFactory<Program>();

            HttpClient client = factory.CreateClient();

            HttpResponseMessage response = await client.GetAsync("/health");

            string body = await response.Content.ReadAsStringAsync();

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual("Healthy", body);
        }
    }
}
