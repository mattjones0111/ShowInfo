namespace Api.Test
{
    using System.Linq;
    using System.Threading.Tasks;
    using Bases;
    using Contracts.V1;
    using NUnit.Framework;

    public class ShowEndpointTests : ApiTestBase
    {
        [Test]
        public async Task CanGetShowsInPages()
        {
            await AddTestDataAsync(40);

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
    }
}
