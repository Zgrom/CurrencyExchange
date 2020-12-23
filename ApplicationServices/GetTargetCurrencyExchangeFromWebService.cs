using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using CurrencyExchangeDomain;
using Newtonsoft.Json;

namespace ApplicationServices
{
    public class GetTargetCurrencyExchangeFromWebService
    {
        private readonly HttpClient _client;
        private const string Uri = "http://data.fixer.io/api/latest?access_key=ecd46d5e28c44d88658a7b109cc29b2a";

        public GetTargetCurrencyExchangeFromWebService(HttpClient httpClient)
        {
            _client = httpClient;
        }
        
        private async Task<HttpResponseMessage> GetContentFromUri(string uri)
        {
            return await _client.GetAsync(uri);
        }

        public async Task<CurrencyExchange> GetCurrencyExchangeFor(
            Currency baseCurrency, 
            Currency targetCurrency, 
            Amount baseCurrencyAmount)
        {
            var currenciesUri = $"{Uri}&base={baseCurrency.Symbol.SymbolValue}&symbols={targetCurrency.Symbol.SymbolValue}";
            var content = await (await GetContentFromUri(currenciesUri)).Content.ReadAsStringAsync();
            var document = JsonConvert.DeserializeObject<CurrencyExchangeRateLoaded>(content);
            if (document.success)
            {
                return CurrencyExchange.Of(
                    baseCurrency,
                    targetCurrency,
                    Timestamp.From(document.timestamp),
                    Rate.From(document.rates[targetCurrency.Symbol.SymbolValue]),
                    baseCurrencyAmount);
            }
            else
            {
                throw new Exception("Some fixer.io error");
            }
        }
        
        private class CurrencyExchangeRateLoaded
        {
            public bool success { get; set; }
            public long timestamp { get; set; }
            public string baseCurrency { get; set; }
            public DateTime date { get; set; }
            public Dictionary<string,double> rates { get; set; }
        }
    }
}