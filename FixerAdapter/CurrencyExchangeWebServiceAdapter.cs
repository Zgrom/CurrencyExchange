using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using CurrencyExchangeDomain;
using FixerAdapter.FixerExceptions;
using FixerAdapter.JsonDeserializeClasses;
using MongoAdapter.DTO;
using Newtonsoft.Json;
using Ports;

namespace FixerAdapter
{
    public class CurrencyExchangeWebServiceAdapter : ICurrencyExchangeWebServicePort
    {
        private readonly string _uriAllAvailableCurrencies;
        private readonly string _uriLatestRates;

        public CurrencyExchangeWebServiceAdapter(
            string uriAllAvailableCurrencies, 
            string uriLatestRates)
        {
            _uriAllAvailableCurrencies = uriAllAvailableCurrencies;
            _uriLatestRates = uriLatestRates;
        }
        
        public async Task<List<Currency>> GetAllAvailableCurrencies()
        {
            var httpClient = new HttpClient();
            var content = 
                await (await httpClient.GetAsync(_uriAllAvailableCurrencies))
                    .Content.ReadAsStringAsync();
            try
            {
                var result = new List<Currency>();
                var document = JsonConvert.DeserializeObject<AllAvailableCurrenciesResult>(content);
                foreach (var documentSymbol in document.symbols)
                {
                    result.Add(
                        Currency.Of(
                            Symbol.From(documentSymbol.Key),
                            CurrencyName.From(documentSymbol.Value) ));
                }
                return result;
            }
            catch (Exception)
            {
                var document = JsonConvert.DeserializeObject<FixerErrorJsonResult>(content);
                throw new FixerErrorException(document.error.code, document.error.type);
            }
        }

        public async Task<LatestRates> GetLatestRates()
        {
            var httpClient = new HttpClient();
            var content = 
                await (await httpClient.GetAsync(_uriLatestRates))
                    .Content.ReadAsStringAsync();
            try
            {
                var document = JsonConvert.DeserializeObject<LatestRatesLoaded>(content);
                var rates = new Dictionary<string, double>();
                foreach (var documentRate in document.rates)
                {
                    var currencySymbol = documentRate.Key;
                    var rate = documentRate.Value;
                    rates.Add(currencySymbol,rate);
                }
                var latestRates = new LatestRatesDto
                {
                    Timestamp = document.timestamp,
                    Rates = rates
                };
                return latestRates.ToDomain();
            }
            catch (JsonException)
            {
                var document = JsonConvert.DeserializeObject<FixerErrorJsonResult>(content);
                throw new FixerErrorException(document.error.code, document.error.type);
            }
        }
    }
}