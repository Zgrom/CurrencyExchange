using System;
using System.Collections.Generic;

namespace CurrencyExchangeDomain
{
    public sealed class CurrencyExchangeOffice
    {
        public List<Currency> AllAvailableCurrencies { get; }
        public Currency BaseCurrency { get; private set; }
        public Currency TargetCurrency { get; private set;}
        public Timestamp Timestamp { get; private set;}
        public Rate Rate { get; private set;}
        public Amount BaseCurrencyAmount { get; private set;}
        public Amount TargetCurrencyAmount { get; private set;}

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

        public static CurrencyExchangeOffice From(List<Currency> allAvailableCurrencies)
        {
            return new CurrencyExchangeOffice(
                allAvailableCurrencies,
                Currency.Of(Symbol.From("NaN"), CurrencyName.From("NaN")), 
                Currency.Of(Symbol.From("NaN"), CurrencyName.From("NaN")),
                Timestamp.From(0), 
                Rate.From(0.0), 
                Amount.From(0.0), 
                Amount.From(0.0));
        }

        public void SetCurrencyExchangeData(
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
            BaseCurrency = baseCurrency;
            TargetCurrency = targetCurrency;
            Timestamp = timestamp;
            Rate = rate;
            BaseCurrencyAmount = baseCurrencyAmount;
            TargetCurrencyAmount = targetCurrencyAmount;
        }

        public bool IsDataTooOld()
        {
            return Timestamp.IsTooOld();
        }
    }
}