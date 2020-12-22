using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using CurrencyExchangeDomain;
using Newtonsoft.Json;

namespace ApplicationServices
{
    public sealed class GetAllAvailableCurrenciesFromWebService
    {
        private readonly HttpClient _client;
        private const string Uri = "http://data.fixer.io/api/symbols?access_key=ecd46d5e28c44d88658a7b109cc29b2a";

        public GetAllAvailableCurrenciesFromWebService(HttpClient httpClient)
        {
            _client = httpClient;
        }

        private async Task<HttpResponseMessage> GetContentFromUri(string uri)
        {
            return await _client.GetAsync(uri);
        }

        public async Task<List<Currency>> GetAll()
        {
            var result = new List<Currency>();
            var content = await (await GetContentFromUri(Uri)).Content.ReadAsStringAsync();
            var document = JsonConvert.DeserializeObject<AllAvailableCurrenciesResult>(content);
            if (document.success)
            {
                foreach (var documentSymbol in document.symbols)
                {
                    result.Add(
                        Currency.Of(
                            Symbol.From(documentSymbol.Key),
                            CurrencyName.From(documentSymbol.Value) ));
                }
            }
            return result;
        }
        
        private class AllAvailableCurrenciesResult
        {
            public bool success { get; set; }
            public Dictionary<string,string> symbols { get; set; }
        }
    }
}