using System;
using CurrencyExchangeDomain.DomainExceptions;

namespace CurrencyExchangeDomain
{
    public sealed class Currency
    {
        public Symbol Symbol { get; }
        public CurrencyName CurrencyName { get; }

        private Currency(Symbol symbol, CurrencyName currencyName)
        {
            Symbol = symbol;
            CurrencyName = currencyName;
        }

        public static Currency Of(Symbol currencySymbol, CurrencyName currencyName)
        {
            if (currencySymbol == null)
            {
                throw new ArgumentCannotBeNullException(nameof(currencySymbol));
            }

            if (currencyName == null)
            {
                throw new ArgumentCannotBeNullException(nameof(currencyName));
            }
            return new Currency(currencySymbol, currencyName);
        }

        private bool Equals(Currency other)
        {
            return Equals(Symbol, other.Symbol) && Equals(CurrencyName, other.CurrencyName);
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is Currency other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Symbol, CurrencyName);
        }
    }
}