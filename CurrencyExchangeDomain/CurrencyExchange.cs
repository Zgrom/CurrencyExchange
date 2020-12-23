using System;
using System.Collections.Generic;
using CurrencyExchangeDomain.DomainExceptions;

namespace CurrencyExchangeDomain
{
    public sealed class CurrencyExchange
    {
        public Currency BaseCurrency { get; }
        public Currency TargetCurrency { get; }
        public Timestamp Timestamp { get; }
        public Rate Rate { get; }
        public Amount BaseCurrencyAmount { get; }
        public Amount TargetCurrencyAmount { get; }

        private CurrencyExchange(
            Currency baseCurrency,
            Currency targetCurrency,
            Timestamp timestamp,
            Rate rate,
            Amount baseCurrencyAmount,
            Amount targetCurrencyAmount)
        {
            BaseCurrency = baseCurrency;
            TargetCurrency = targetCurrency;
            Timestamp = timestamp;
            Rate = rate;
            BaseCurrencyAmount = baseCurrencyAmount;
            TargetCurrencyAmount = targetCurrencyAmount;
        }

        public static CurrencyExchange Of(
            Currency baseCurrency,
            Currency targetCurrency,
            Timestamp timestamp,
            Rate rate,
            Amount baseCurrencyAmount)
        {
            if (baseCurrency == null)
            {
                throw new ArgumentCannotBeNullException(nameof(baseCurrency));
            }
            if (targetCurrency == null)
            {
                throw new ArgumentCannotBeNullException(nameof(targetCurrency));
            }
            if (timestamp == null)
            {
                throw new ArgumentCannotBeNullException(nameof(timestamp));
            }
            if (rate == null)
            {
                throw new ArgumentCannotBeNullException(nameof(rate));
            }
            if (baseCurrencyAmount == null)
            {
                throw new ArgumentCannotBeNullException(nameof(baseCurrencyAmount));
            }
            
            var targetCurrencyAmount = baseCurrencyAmount.MultiplyWithRate(rate);
            
            return new CurrencyExchange(
                baseCurrency,
                targetCurrency,
                timestamp,
                rate,
                baseCurrencyAmount,
                targetCurrencyAmount);
        }

        public bool IsDataTooOld()
        {
            return Timestamp.IsTooOld();
        }

        private bool Equals(CurrencyExchange other)
        {
            return Equals(BaseCurrency, other.BaseCurrency) && Equals(TargetCurrency, other.TargetCurrency) && Equals(Timestamp, other.Timestamp);
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is CurrencyExchange other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(BaseCurrency, TargetCurrency, Timestamp);
        }
    }
}