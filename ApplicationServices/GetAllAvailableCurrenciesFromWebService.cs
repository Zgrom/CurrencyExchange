using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using CurrencyExchangeDomain;
using Newtonsoft.Json;
using ApplicationServices.ApplicationServicesExceptions;
using ApplicationServices.JsonDeserializeClasses;
using Microsoft.Extensions.Configuration;

namespace ApplicationServices
{
    public sealed class GetAllAvailableCurrenciesFromWebService
    {
        private readonly IConfiguration _configuration; 
        private readonly HttpClient _client;

        public GetAllAvailableCurrenciesFromWebService(
            HttpClient httpClient,
            IConfiguration configuration)
        {
            _client = httpClient;
            _configuration = configuration;
        }

        private async Task<HttpResponseMessage> GetContentFromUri(string uri)
        {
            return await _client.GetAsync(uri);
        }

        public async Task<List<Currency>> GetAll()
        {
            var result = new List<Currency>();
            var content = 
                await (await GetContentFromUri(_configuration["Fixer.Io.Uris:Symbols"]))
                    .Content.ReadAsStringAsync();
            try
            {
                var document = JsonConvert.DeserializeObject<AllAvailableCurrenciesResult>(content);
                foreach (var documentSymbol in document.symbols)
                {
                    result.Add(
                        Currency.Of(
                            Symbol.From(documentSymbol.Key),
                            CurrencyName.From(documentSymbol.Value) ));
                }
            }
            catch (Exception)
            {
                var document = JsonConvert.DeserializeObject<FixerErrorJsonResult>(content);
                throw new FixerErrorException(document.error.code, document.error.type);
            }
            
            return result;
        }
        
    }
}