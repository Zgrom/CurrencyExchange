using System.Threading.Tasks;
using Ports;

namespace ApplicationServices
{
    public sealed class DatabaseDeleteAllAvailableCurrenciesService
    {
        private readonly ICurrencyExchangeRepository _currencyExchangeRepository;

        public DatabaseDeleteAllAvailableCurrenciesService(ICurrencyExchangeRepository currencyExchangeRepository)
        {
            _currencyExchangeRepository = currencyExchangeRepository;
        }
        
        public async Task DeleteAll()
        {
            await _currencyExchangeRepository.DeleteAllAvailableCurrencies();
        }
    }
}