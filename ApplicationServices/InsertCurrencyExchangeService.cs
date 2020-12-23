using System.Threading.Tasks;
using CurrencyExchangeDomain;
using Ports;

namespace ApplicationServices
{
    public sealed class InsertCurrencyExchangeService
    {
        private readonly ICurrencyExchangeRepository _currencyExchangeRepository;

        public InsertCurrencyExchangeService(ICurrencyExchangeRepository currencyExchangeRepository)
        {
            _currencyExchangeRepository = currencyExchangeRepository;
        }

        public async Task Insert(CurrencyExchange currencyExchange) =>
            await _currencyExchangeRepository.InsertCurrencyExchange(currencyExchange);
    }
}