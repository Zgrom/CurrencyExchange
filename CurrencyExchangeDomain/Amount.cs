using System;

namespace CurrencyExchangeDomain
{
    public sealed class Amount
    {
        public double AmountValue { get; }

        private Amount(double amountValue)
        {
            AmountValue = amountValue;
        }

        public static Amount From(double amount)
        {
            if (amount < 0.0)
            {
                throw new ArgumentException(
                $"{nameof(amount)} cannot have negative value.");
            }
            return new Amount(amount);
        }

        public Amount MultiplyWithRate(Rate rate)
        {
            return new Amount(AmountValue*rate.RateValue);
        }

        private bool Equals(Amount other)
        {
            return AmountValue.Equals(other.AmountValue);
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is Amount other && Equals(other);
        }

        public override int GetHashCode()
        {
            return AmountValue.GetHashCode();
        }
    }
}