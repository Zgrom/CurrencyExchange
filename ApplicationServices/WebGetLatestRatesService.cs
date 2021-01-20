using System.Threading.Tasks;
using CurrencyExchangeDomain;
using Ports;

namespace ApplicationServices
{
    public sealed class WebGetLatestRatesService
    {
        private readonly ICurrencyExchangeWebServicePort _currencyExchangeWebServicePort;

        public WebGetLatestRatesService(
            ICurrencyExchangeWebServicePort currencyExchangeWebServicePort)
        {
            _currencyExchangeWebServicePort = currencyExchangeWebServicePort;
        }

        public async Task<LatestRates> GetAll()
        {
            return await _currencyExchangeWebServicePort.GetLatestRates();
        }
    }
}