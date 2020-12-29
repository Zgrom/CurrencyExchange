using System.Threading.Tasks;
using CurrencyExchangeDomain;
using Ports;
using Ports.RepositoryExceptions;

namespace ApplicationServices
{
    public class GetTargetCurrencyAmountFromDatabaseService
    {
        private readonly ICurrencyExchangeRepository _currencyExchangeRepository;

        public GetTargetCurrencyAmountFromDatabaseService(
            ICurrencyExchangeRepository currencyExchangeRepository)
        {
            _currencyExchangeRepository = currencyExchangeRepository;
        }

        public async Task<double> GetAmount(
            Currency baseCurrency,
            Currency targetCurrency,
            Amount baseCurrencyAmount)
        {
            var latestRates = await _currencyExchangeRepository.GetLatestRates();
            if (latestRates.IsTooOld())
            {
                await _currencyExchangeRepository.DeleteLatestRates(latestRates);
                throw new NoValidCurrencyExchangeRateException();
            }

            var currencyExchange = CurrencyExchange.Of(
                baseCurrency,
                targetCurrency,
                latestRates.Timestamp,
                latestRates.GetRateFor(baseCurrency, targetCurrency),
                baseCurrencyAmount);
            
            return currencyExchange.TargetCurrencyAmount.AmountValue;
        }
    }
}