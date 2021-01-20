using System.Collections.Generic;
using CurrencyExchangeDomain.DomainExceptions;

namespace CurrencyExchangeDomain
{
    public sealed class LatestRates
    {
        public Timestamp Timestamp { get; }
        public Dictionary<Symbol,Rate> Rates { get; }

        private LatestRates(Timestamp timestamp, Dictionary<Symbol, Rate> rates)
        {
            Timestamp = timestamp;
            Rates = rates;
        }

        public static LatestRates Of(Timestamp timestamp, Dictionary<Symbol, Rate> rates)
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

        public Rate GetRateFor(Symbol baseCurrency, Symbol targetCurrency)
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

            return Rate.From(targetCurrencyRate.RateValue/baseCurrencyRate.RateValue);
        }

        private bool Equals(LatestRates other)
        {
            return Equals(Timestamp, other.Timestamp);
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is LatestRates other && Equals(other);
        }

        public override int GetHashCode()
        {
            return (Timestamp != null ? Timestamp.GetHashCode() : 0);
        }
    }
}