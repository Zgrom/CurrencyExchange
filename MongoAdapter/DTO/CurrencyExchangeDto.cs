using CurrencyExchangeDomain;

namespace MongoAdapter.DTO
{
    public sealed class CurrencyExchangeDto
    {
        public string Id { get; set; }
        public CurrencyAsPropertyDto BaseCurrency { get; set; }
        public CurrencyAsPropertyDto TargetCurrency { get; set; }
        public long Timestamp { get; set; }
        public double Rate { get; set; }

        public CurrencyExchange ToDomain(Amount currencyExchangeBaseCurrencyAmount)
        {
            var currencyExchangeBaseCurrency = Currency.Of(
                Symbol.From(BaseCurrency.Symbol),
                CurrencyName.From(BaseCurrency.CurrencyName));
            var currencyExchangeTargetCurrency = Currency.Of(
                Symbol.From(TargetCurrency.Symbol),
                CurrencyName.From(TargetCurrency.CurrencyName));
            var currencyExchangeTimestamp = CurrencyExchangeDomain.Timestamp.From(Timestamp);
            var currencyExchangeRate = CurrencyExchangeDomain.Rate.From(Rate);

            return CurrencyExchange.Of(
                currencyExchangeBaseCurrency,
                currencyExchangeTargetCurrency,
                currencyExchangeTimestamp,
                currencyExchangeRate,
                currencyExchangeBaseCurrencyAmount);
        }
    }
}