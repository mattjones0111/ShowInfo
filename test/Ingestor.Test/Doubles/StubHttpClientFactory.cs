namespace Ingestor.Test.Doubles
{
    using System;
    using System.Net.Http;

    public sealed class StubHttpClientFactory : IHttpClientFactory
    {
        readonly HttpClient _client;

        public StubHttpClientFactory(HttpClient client)
        {
            _client = client;
        }

        public HttpClient CreateClient(string name)
        {
            _client.BaseAddress = new Uri("https://api.tvmaze.com.local");

            return _client;
        }
    }
}
