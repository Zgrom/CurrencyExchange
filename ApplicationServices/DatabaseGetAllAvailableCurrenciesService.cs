using System.Collections.Generic;
using System.Threading.Tasks;
using CurrencyExchangeDomain;
using Ports;

namespace ApplicationServices
{
    public sealed class DatabaseGetAllAvailableCurrenciesService
    {
        private readonly ICurrencyExchangeRepository _currencyExchangeRepository;

        public DatabaseGetAllAvailableCurrenciesService(ICurrencyExchangeRepository currencyExchangeRepository)
        {
            _currencyExchangeRepository = currencyExchangeRepository;
        }

        public async Task<List<Currency>> GetAll()
        {
            return await _currencyExchangeRepository.GetAllAvailableCurrencies();
        }
    }
}