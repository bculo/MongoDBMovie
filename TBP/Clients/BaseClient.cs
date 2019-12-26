using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace TBP.Clients
{
    public abstract class BaseClient
    {
        protected readonly HttpClient _client;
        public string BaseURI => _client?.BaseAddress?.ToString() ?? throw new ArgumentNullException(nameof(BaseURI));

        public BaseClient(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _client = client;
            _client.DefaultRequestHeaders.Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public void SetBaseUri(string uri)
        {
            _client.BaseAddress = new Uri(uri);
        }
    }
}
