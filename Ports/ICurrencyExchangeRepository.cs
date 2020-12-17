using System.Collections.Generic;
using System.Threading.Tasks;
using CurrencyExchangeDomain;

namespace Ports
{
    public interface ICurrencyExchangeRepository
    {
        Task<CurrencyExchange> GetCurrencyExchangeRate(
            Currency baseCurrency, 
            Currency targetCurrency,
            Amount baseCurrencyAmount);
        Task InsertCurrencyExchange(CurrencyExchange currencyExchange);
        Task DeleteCurrencyExchange(CurrencyExchange currencyExchange);
        Task<List<Currency>> GetAllAvailableCurrencies();
        Task InsertAllAvailableCurrencies(List<Currency> allAvailableCurrencies);
    }
}