using System.Collections.Generic;
using System.Threading.Tasks;
using CurrencyExchangeDomain;

namespace Ports
{
    public interface ICurrencyExchangeRepository
    {
        Task<LatestRates> GetLatestRates();
        Task InsertLatestRates(LatestRates latestRates);
        Task DeleteLatestRates(LatestRates latestRates);
        Task<List<Currency>> GetAllAvailableCurrencies();
        Task InsertAllAvailableCurrencies(List<Currency> allAvailableCurrencies);
    }
}