using System.Collections.Generic;
using System.Threading.Tasks;
using CurrencyExchangeDomain;
using Ports;

namespace ApplicationServices
{
    public class DatabaseInsertAllAvailableCurrenciesService
    {
        private readonly ICurrencyExchangeRepository _currencyExchangeRepository;

        public DatabaseInsertAllAvailableCurrenciesService(
            ICurrencyExchangeRepository currencyExchangeRepository)
        {
            _currencyExchangeRepository = currencyExchangeRepository;
        }

        public async Task InsertAll(List<Currency> allAvailableCurrencies) =>
            await _currencyExchangeRepository.InsertAllAvailableCurrencies(allAvailableCurrencies);
    }
}