namespace Api.Test
{
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Bases;
    using Contracts.V1;
    using NUnit.Framework;

    public class ShowEndpointTests : ApiTestBase
    {
        [OneTimeSetUp]
        public async Task Setup()
        {
            await AddTestDataAsync(100);
        }

        [Test]
        public async Task CanGetShowsInPagesAsync()
        {
            Show[] page1 = await GetShowsAsync(1, 20);

            Show[] page2 = await GetShowsAsync(2, 20);

            Assert.AreEqual(20, page1.Length);
            Assert.AreEqual(20, page2.Length);

            // make sure that no item that appears in page 1
            // also appears in page 2
            Assert.IsFalse(
                page1
                    .Select(x => x.Id)
                    .Intersect(page2.Select(x => x.Id))
                    .Any());
        }

        [TestCase(-1, 20)]
        [TestCase(1, -1)]
        [TestCase(1, 51)]
        public async Task CannotGetInvalidPagingAsync(int pageNumber, int pageSize)
        {
            HttpClient client = GetClient();

            HttpResponseMessage response =
                await client.GetAsync($"/shows?pageNumber={pageNumber}&pageSize={pageSize}");

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
