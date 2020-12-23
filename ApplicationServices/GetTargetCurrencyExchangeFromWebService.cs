using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ApplicationServices.ApplicationServicesExceptions;
using ApplicationServices.JsonDeserializeClasses;
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
            try
            {
                var document = JsonConvert.DeserializeObject<CurrencyExchangeRateLoaded>(content);
                return CurrencyExchange.Of(
                    baseCurrency,
                    targetCurrency,
                    Timestamp.From(document.timestamp),
                    Rate.From(document.rates[targetCurrency.Symbol.SymbolValue]),
                    baseCurrencyAmount);
            }
            catch (Exception)
            {
                var document = JsonConvert.DeserializeObject<FixerErrorJsonResult>(content);
                throw new FixerErrorException(document.error.code, document.error.info);
            }
        }
       
    }
}