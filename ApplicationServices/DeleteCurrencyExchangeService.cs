using System.Threading.Tasks;
using CurrencyExchangeDomain;
using Ports;

namespace ApplicationServices
{
    public sealed class DeleteCurrencyExchangeService
    {
        private readonly ICurrencyExchangeRepository _currencyExchangeRepository;

        public DeleteCurrencyExchangeService(ICurrencyExchangeRepository currencyExchangeRepository)
        {
            _currencyExchangeRepository = currencyExchangeRepository;
        }

        public async Task Delete(CurrencyExchange currencyExchange) =>
            await _currencyExchangeRepository.DeleteCurrencyExchange(currencyExchange);
    }
}