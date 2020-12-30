using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ApplicationServices.ApplicationServicesExceptions;
using ApplicationServices.JsonDeserializeClasses;
using CurrencyExchangeDomain;
using Microsoft.Extensions.Configuration;
using MongoAdapter.DTO;
using Newtonsoft.Json;
using JsonException = System.Text.Json.JsonException;

namespace ApplicationServices
{
    public sealed class GetLatestRatesFromWebService
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _client;
        private readonly GetAllAvailableCurrenciesService _getAllAvailableCurrenciesService;

        public GetLatestRatesFromWebService(
            HttpClient httpClient,
            GetAllAvailableCurrenciesService getAllAvailableCurrenciesService,
            IConfiguration configuration)
        {
            _client = httpClient;
            _getAllAvailableCurrenciesService = getAllAvailableCurrenciesService;
            _configuration = configuration;
        }
        
        private async Task<HttpResponseMessage> GetContentFromUri(string uri)
        {
            return await _client.GetAsync(uri);
        }

        public async Task<LatestRates> GetLatestRates()
        {
            var content = 
                await (await GetContentFromUri(_configuration["Fixer.Io.Uris:Latest"]))
                    .Content.ReadAsStringAsync();
            try
            {
                var document = JsonConvert.DeserializeObject<LatestRatesLoaded>(content);
                var allAvailableCurrencies = await _getAllAvailableCurrenciesService.GetAll();
                var rates = new Dictionary<CurrencyAsPropertyDto, double>();
                foreach (var documentRate in document.rates)
                {
                    var currencyAsProperty = new CurrencyAsPropertyDto
                    {
                        CurrencyName = allAvailableCurrencies
                            .First(curr => curr.Symbol.SymbolValue == documentRate.Key)
                            .CurrencyName.CurrencyNameValue,
                        Symbol = documentRate.Key
                    };
                    var rate = documentRate.Value;
                    rates.Add(currencyAsProperty,rate);
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