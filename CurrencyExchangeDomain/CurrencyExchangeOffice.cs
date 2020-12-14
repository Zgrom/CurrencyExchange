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
    }
}