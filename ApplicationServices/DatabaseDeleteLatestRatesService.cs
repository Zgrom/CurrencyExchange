using System.Threading.Tasks;
using CurrencyExchangeDomain;
using Ports;

namespace ApplicationServices
{
    public sealed class DatabaseDeleteLatestRatesService
    {
        private readonly ICurrencyExchangeRepository _currencyExchangeRepository;

        public DatabaseDeleteLatestRatesService(ICurrencyExchangeRepository currencyExchangeRepository)
        {
            _currencyExchangeRepository = currencyExchangeRepository;
        }
        
        public async Task DeleteAll(LatestRates latestRates)
        {
            await _currencyExchangeRepository.DeleteLatestRates(latestRates);
        }
    }
}