using System.Collections.Generic;
using System.Threading.Tasks;
using CurrencyExchangeDomain;

namespace Ports
{
    public interface ICurrencyExchangeWebServicePort
    {
        Task<List<Currency>> GetAllAvailableCurrencies();
        Task<LatestRates> GetLatestRates();
    }
}