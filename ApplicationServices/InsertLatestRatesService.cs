using System.Threading.Tasks;
using CurrencyExchangeDomain;
using Ports;

namespace ApplicationServices
{
    public sealed class InsertLatestRatesService
    {
        private readonly ICurrencyExchangeRepository _currencyExchangeRepository;

        public InsertLatestRatesService(ICurrencyExchangeRepository currencyExchangeRepository)
        {
            _currencyExchangeRepository = currencyExchangeRepository;
        }

        public async Task Insert(LatestRates latestRates)
            => await _currencyExchangeRepository.InsertLatestRates(latestRates);
    }
}