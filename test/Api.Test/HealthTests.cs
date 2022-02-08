namespace Api.Test
{
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Bases;
    using NUnit.Framework;

    public class HealthTests : ApiTestBase
    {
        [Test]
        public async Task ApplicationIsHealthy()
        {
            HttpClient client = GetClient();

            HttpResponseMessage response = await client.GetAsync("/health");

            string body = await response.Content.ReadAsStringAsync();

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual("Healthy", body);
        }
    }
}
