using System;

namespace CurrencyExchangeDomain.Tests
{
    public sealed class CurrencyName
    {
        public string CurrencyNameValue { get; }

        private CurrencyName(string currencyNameValue)
        {
            CurrencyNameValue = currencyNameValue;
        }

        public static CurrencyName From(string currencyName)
        {
            if (string.IsNullOrWhiteSpace(currencyName))
            {
                throw new ArgumentException(
                    $"{nameof(currencyName)} cannot be null or white space.");
            }
            
            return new CurrencyName(currencyName);
        }

        private bool Equals(CurrencyName other)
        {
            return CurrencyNameValue == other.CurrencyNameValue;
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is CurrencyName other && Equals(other);
        }

        public override int GetHashCode()
        {
            return (CurrencyNameValue != null ? CurrencyNameValue.GetHashCode() : 0);
        }
    }
}