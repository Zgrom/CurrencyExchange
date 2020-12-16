using System;
using System.Collections.Generic;

namespace CurrencyExchangeDomain
{
    public sealed class CurrencyExchangeOffice
    {
        public List<Currency> AllAvailableCurrencies { get; }
        public Currency BaseCurrency { get; }
        public Currency TargetCurrency { get; }
        private Timestamp Timestamp { get; }
        public Rate Rate { get; }
        public Amount BaseCurrencyAmount { get; }
        public Amount TargetCurrencyAmount { get; }

        private CurrencyExchangeOffice(
            List<Currency> allAvailableCurrencies,
            Currency baseCurrency,
            Currency targetCurrency,
            Timestamp timestamp,
            Rate rate,
            Amount baseCurrencyAmount,
            Amount targetCurrencyAmount)
        {
            AllAvailableCurrencies = allAvailableCurrencies;
            BaseCurrency = baseCurrency;
            TargetCurrency = targetCurrency;
            Timestamp = timestamp;
            Rate = rate;
            BaseCurrencyAmount = baseCurrencyAmount;
            TargetCurrencyAmount = targetCurrencyAmount;
        }

        public static CurrencyExchangeOffice Of(
            List<Currency> allAvailableCurrencies,
            Currency baseCurrency,
            Currency targetCurrency,
            Timestamp timestamp,
            Rate rate,
            Amount baseCurrencyAmount)
        {
            if (baseCurrency == null)
            {
                throw new ArgumentException($"{nameof(baseCurrency)} cannot be null.");
            }
            if (targetCurrency == null)
            {
                throw new ArgumentException($"{nameof(targetCurrency)} cannot be null.");
            }
            if (timestamp == null)
            {
                throw new ArgumentException($"{nameof(timestamp)} cannot be null.");
            }
            if (rate == null)
            {
                throw new ArgumentException($"{nameof(rate)} cannot be null.");
            }
            if (baseCurrencyAmount == null)
            {
                throw new ArgumentException($"{nameof(baseCurrencyAmount)} cannot be null.");
            }
            var targetCurrencyAmount = baseCurrencyAmount.MultiplyWithRate(rate);
            return new CurrencyExchangeOffice(
                allAvailableCurrencies,
                baseCurrency,
                targetCurrency,
                timestamp,
                rate,
                baseCurrencyAmount,
                targetCurrencyAmount);
        }
    }
}