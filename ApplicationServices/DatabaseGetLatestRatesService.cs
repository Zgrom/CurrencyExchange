using System.Threading.Tasks;
using CurrencyExchangeDomain;
using Ports;

namespace ApplicationServices
{
    public sealed class DatabaseGetLatestRatesService
    {
        private readonly ICurrencyExchangeRepository _currencyExchangeRepository;

        public DatabaseGetLatestRatesService(ICurrencyExchangeRepository currencyExchangeRepository)
        {
            _currencyExchangeRepository = currencyExchangeRepository;
        }
        
        public async Task<LatestRates> GetAll()
        {
            return await _currencyExchangeRepository.GetLatestRates();
        }
    }
}