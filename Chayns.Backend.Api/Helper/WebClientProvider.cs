using System.Net.Http;
using System.Net.Http.Headers;

namespace Chayns.Backend.Api.Helper
{
    internal static class WebClientProvider
    {
        private static readonly HttpClient Client = new HttpClient
        {
            DefaultRequestHeaders =
            {
                UserAgent =
                {
                    new ProductInfoHeaderValue("ChaynsBackendApi", "1.0")
                }
            }
        };

        internal static HttpClient GetClient() => Client;
    }
}