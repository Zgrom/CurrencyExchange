using System;
using CurrencyExchangeDomain.DomainExceptions;

namespace CurrencyExchangeDomain
{
    public sealed class Rate
    {
        public double RateValue { get; }

        private Rate(double rateValue)
        {
            RateValue = rateValue;
        }

        public static Rate From(double rate)
        {
            if (rate < 0.0)
            {
                throw new ArgumentCannotHaveNegativeValueException(nameof(rate));
            }
            return new Rate(rate);
        }

        private bool Equals(Rate other)
        {
            return RateValue.Equals(other.RateValue);
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is Rate other && Equals(other);
        }

        public override int GetHashCode()
        {
            return RateValue.GetHashCode();
        }
    }
}