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

    }
}