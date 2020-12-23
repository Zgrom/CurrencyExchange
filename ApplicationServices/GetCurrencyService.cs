using System;
using System.Linq;
using System.Threading.Tasks;
using CurrencyExchangeDomain;
using Ports.RepositoryExceptions;

namespace ApplicationServices
{
    public sealed class GetCurrencyService
    {
        private readonly GetAllAvailableCurrenciesService _getAllAvailableCurrenciesService;

        public GetCurrencyService(GetAllAvailableCurrenciesService getAllAvailableCurrenciesService)
        {
            _getAllAvailableCurrenciesService = getAllAvailableCurrenciesService;
        }

        public async Task<Currency> GetBySymbol(string currencySymbol)
        {
            var allAvailableCurrencies = await _getAllAvailableCurrenciesService.GetAll();
            foreach (var currency in allAvailableCurrencies.Where(currency => currency.Symbol.SymbolValue == currencySymbol))
            {
                return currency;
            }
            throw new CurrencySymbolNotAvailableException(currencySymbol);
        }
    }
}