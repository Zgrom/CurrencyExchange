using System;
using System.Threading.Tasks;
using CurrencyExchangeDomain;
using Ports;
using Ports.RepositoryExceptions;

namespace ApplicationServices
{
    public class GetTargetCurrencyAmountFromDatabaseService
    {
        private readonly ICurrencyExchangeRepository _currencyExchangeRepository;
        private readonly DeleteCurrencyExchangeService _deleteCurrencyExchangeService;

        public GetTargetCurrencyAmountFromDatabaseService(
            ICurrencyExchangeRepository currencyExchangeRepository,
            DeleteCurrencyExchangeService deleteCurrencyExchangeService)
        {
            _currencyExchangeRepository = currencyExchangeRepository;
            _deleteCurrencyExchangeService = deleteCurrencyExchangeService;
        }

        public async Task<double> GetAmount(
            Currency baseCurrency,
            Currency targetCurrency,
            Amount baseCurrencyAmount)
        {
            var currencyExchange = await _currencyExchangeRepository
                .GetCurrencyExchangeRate(baseCurrency, targetCurrency, baseCurrencyAmount);
            if (currencyExchange.IsDataTooOld())
            {
                await _deleteCurrencyExchangeService.Delete(currencyExchange);
                throw new NoValidCurrencyExchangeRateException();
            }
            
            return currencyExchange.TargetCurrencyAmount.AmountValue;
        }
    }
}