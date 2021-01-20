using System.Collections.Generic;
using System.Threading.Tasks;
using CurrencyExchangeDomain;
using Ports;

namespace ApplicationServices
{
    public sealed class WebGetAllAvailableCurrenciesService
    {
        private readonly ICurrencyExchangeWebServicePort _currencyExchangeWebServicePort;

        public WebGetAllAvailableCurrenciesService(
            ICurrencyExchangeWebServicePort currencyExchangeWebServicePort)
        {
            _currencyExchangeWebServicePort = currencyExchangeWebServicePort;
        }

        public async Task<List<Currency>> GetAll()
        {
            return await _currencyExchangeWebServicePort.GetAllAvailableCurrencies();
        }
    }
}