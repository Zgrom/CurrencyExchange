using System.Collections.Generic;
using CurrencyExchangeDomain.DomainExceptions;

namespace CurrencyExchangeDomain
{
    public sealed class LatestRates
    {
        public Timestamp Timestamp { get; }
        public Dictionary<Currency,Rate> Rates { get; }

        private LatestRates(Timestamp timestamp, Dictionary<Currency, Rate> rates)
        {
            Timestamp = timestamp;
            Rates = rates;
        }

        public static LatestRates Of(Timestamp timestamp, Dictionary<Currency, Rate> rates)
        {
            if (timestamp == null)
            {
                throw new ArgumentCannotBeNullException(nameof(timestamp));
            }

            if (rates == null)
            {
                throw new ArgumentCannotBeNullException(nameof(rates));
            }
            return new LatestRates(timestamp, rates);
        }

        public Rate GetRateFor(Currency baseCurrency, Currency targetCurrency)
        {
            if (baseCurrency == null)
            {
                throw new ArgumentCannotBeNullException(nameof(baseCurrency));
            }

            if (targetCurrency == null)
            {
                throw new ArgumentCannotBeNullException(nameof(targetCurrency));
            }
            
            var baseCurrencyRate = Rates[baseCurrency];
            var targetCurrencyRate = Rates[targetCurrency];

            if (targetCurrencyRate.RateValue <= 0.0)
            {
                throw new BadRateDataException(nameof(targetCurrency.Symbol));
            }
            
            return Rate.From(targetCurrencyRate.RateValue/baseCurrencyRate.RateValue);
        }
    }
}