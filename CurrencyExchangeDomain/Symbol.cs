using System;

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
            if (currencySymbol == null)
            {
                throw new ArgumentNullException($"{nameof(currencySymbol)} cannot be null.");
            }

            if (currencySymbol.Length != 3)
            {
                throw new ArgumentOutOfRangeException(
                    $"{nameof(currencySymbol)} must have length of exactly 3.");
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