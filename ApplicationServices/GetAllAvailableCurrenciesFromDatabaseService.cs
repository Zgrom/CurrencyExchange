using System.Collections.Generic;
using System.Threading.Tasks;
using CurrencyExchangeDomain;
using Ports;

namespace ApplicationServices
{
    public sealed class GetAllAvailableCurrenciesFromDatabaseService
    {
        private readonly ICurrencyExchangeRepository _currencyExchangeRepository;

        public GetAllAvailableCurrenciesFromDatabaseService(ICurrencyExchangeRepository currencyExchangeRepository)
        {
            _currencyExchangeRepository = currencyExchangeRepository;
        }

        public async Task<List<Currency>> GetAll()
        {
            return await _currencyExchangeRepository.GetAllAvailableCurrencies();
        }
    }
}