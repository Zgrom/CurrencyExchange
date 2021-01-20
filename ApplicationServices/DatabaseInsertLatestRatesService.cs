using System.Threading.Tasks;
using CurrencyExchangeDomain;
using Ports;

namespace ApplicationServices
{
    public sealed class DatabaseInsertLatestRatesService
    {
        private readonly ICurrencyExchangeRepository _currencyExchangeRepository;

        public DatabaseInsertLatestRatesService(
            ICurrencyExchangeRepository currencyExchangeRepository)
        {
            _currencyExchangeRepository = currencyExchangeRepository;
        }
        
        public async Task InsertAll(LatestRates latestRates) =>
            await _currencyExchangeRepository.InsertLatestRates(latestRates);
    }
}