using System;
using CurrencyExchangeDomain.DomainExceptions;

namespace CurrencyExchangeDomain
{
    public sealed class Symbol
    {
        public string SymbolValue { get; }

        private Symbol(string symbolValue)
        {
            SymbolValue = symbolValue;
        }

        public static Symbol From(string currencySymbol)
        {
            if (string.IsNullOrWhiteSpace(currencySymbol))
            {
                throw new ArgumentCannotBeNullOrWhitespaceException(nameof(currencySymbol));
            }

            if (currencySymbol.Length != 3)
            {
                throw new ArgumentMustHaveExactLengthValueException(nameof(currencySymbol));
            }
            
            return new Symbol(currencySymbol);
        }

        private bool Equals(Symbol other)
        {
            return SymbolValue == other.SymbolValue;
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is Symbol other && Equals(other);
        }

        public override int GetHashCode()
        {
            return (SymbolValue != null ? SymbolValue.GetHashCode() : 0);
        }
    }
}