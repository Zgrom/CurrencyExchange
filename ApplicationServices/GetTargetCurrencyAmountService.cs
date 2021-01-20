using System.Threading.Tasks;
using CurrencyExchangeDomain;

namespace ApplicationServices
{
    public sealed class GetTargetCurrencyAmountService
    {
        private readonly DatabaseGetLatestRatesService _databaseGetLatestRatesService;

        public GetTargetCurrencyAmountService(
            DatabaseGetLatestRatesService databaseGetLatestRatesService)
        {
            _databaseGetLatestRatesService = databaseGetLatestRatesService;
        }

        public async Task<double> GetAmount(
            Symbol baseCurrency, 
            Symbol targetCurrency, 
            Amount baseCurrencyAmount)
        {
            var latestRates = await _databaseGetLatestRatesService.GetAll();
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