using System;

namespace CurrencyExchangeDomain
{
    public sealed class Timestamp
    {
        public long TimestampValue { get; }

        private Timestamp(long timestampValue)
        {
            TimestampValue = timestampValue;
        }

        public static Timestamp From(long timestamp)
        {
            if (timestamp < 0)
            {
                throw new ArgumentOutOfRangeException(
                    $"{nameof(timestamp)} cannot have negative value.");
            }
            return new Timestamp(timestamp);
        }

        private static Timestamp Now()
        {
            var timestampNow = DateTimeOffset.Now.ToUnixTimeSeconds();
            return new Timestamp(timestampNow);
        }

        public bool IsTooOld()
        {
            const int period = 86400; // exactly one day in seconds
            return Now().TimestampValue - TimestampValue > period;
        }

        public bool Equals(Timestamp other)
        {
            return TimestampValue == other.TimestampValue;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Timestamp) obj);
        }

        public override int GetHashCode()
        {
            return TimestampValue.GetHashCode();
        }
    }
}