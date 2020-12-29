using System.Threading.Tasks;
using CurrencyExchangeDomain;
using Ports.RepositoryExceptions;

namespace ApplicationServices
{
    public sealed class GetTargetCurrencyAmountService
    {
        private readonly GetTargetCurrencyAmountFromDatabaseService _getTargetCurrencyAmountFromDatabaseService;
        private readonly GetLatestRatesFromWebService _getLatestRatesFromWebService;
        private readonly InsertLatestRatesService _insertLatestRatesService;

        public GetTargetCurrencyAmountService(
            GetTargetCurrencyAmountFromDatabaseService getTargetCurrencyAmountFromDatabaseService,
            GetLatestRatesFromWebService getLatestRatesFromWebService,
            InsertLatestRatesService insertLatestRatesService)
        {
            _getTargetCurrencyAmountFromDatabaseService = getTargetCurrencyAmountFromDatabaseService;
            _getLatestRatesFromWebService = getLatestRatesFromWebService;
            _insertLatestRatesService = insertLatestRatesService;
        }

        public async Task<double> GetAmount(
            Currency baseCurrency, 
            Currency targetCurrency, 
            Amount baseCurrencyAmount)
        {
            var targetCurrencyAmount = 0.0;
            try
            {
                targetCurrencyAmount = 
                    await _getTargetCurrencyAmountFromDatabaseService
                        .GetAmount(baseCurrency, targetCurrency, baseCurrencyAmount);
            }
            catch (NoValidCurrencyExchangeRateException)
            {
                var latestRates = await _getLatestRatesFromWebService.GetLatestRates();
                await _insertLatestRatesService.Insert(latestRates);
                var currencyExchange = CurrencyExchange.Of(
                    baseCurrency,
                    targetCurrency,
                    latestRates.Timestamp,
                    latestRates.GetRateFor(baseCurrency, targetCurrency),
                    baseCurrencyAmount);
                    targetCurrencyAmount = currencyExchange.TargetCurrencyAmount.AmountValue;
            }

            return targetCurrencyAmount;
        }
    }
}