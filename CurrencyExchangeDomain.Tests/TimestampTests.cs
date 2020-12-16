using System;
using CurrencyExchangeDomain.Tests;
using FluentAssertions;
using Xunit;

namespace CurrencyExchangeDomain.Tests.Tests
{
    public sealed class TimestampTests
    {
        [Fact]
        public void creating_Timestamp_object_for_valid_argument_value_should_be_successful()
        {
            var timestampNow = DateTimeOffset.Now.ToUnixTimeSeconds();
            var timestamp = Timestamp.From(timestampNow);

            timestamp.TimestampValue.Should().Be(timestampNow);
        }
        
        [Fact]
        public void creating_Timestamp_object_for_invalid_argument_value_should_fail()
        {
            Action action = () => Timestamp.From(-11);

            action.Should().Throw<ArgumentException>()
                .WithMessage("timestamp cannot have negative value.");
        }

        [Fact]
        public void for_timestamps_representing_dayTimes_beyond_yesterday_method_IsTooOld_should_return_true()
        {
            var longAgo = new DateTimeOffset(
                2020, 
                12, 
                14, 
                8, 
                0, 
                0, 
                new TimeSpan(1, 0, 0))
                .ToUnixTimeSeconds();
            var timestampLongAgo = Timestamp.From(longAgo);

            timestampLongAgo.IsTooOld().Should().BeTrue();
        }

        [Fact]
        public void for_timestamps_representing_dayTimes_today_method_IsTooOld_should_return_false()
        {
            var notLongAgo = DateTimeOffset.Now.ToUnixTimeSeconds();
            var timestampNotLongAgo = Timestamp.From(notLongAgo);
            System.Threading.Thread.Sleep(2000);

            timestampNotLongAgo.IsTooOld().Should().BeFalse();
        }
        
        [Fact]
        public void Timestamp_objects_with_equal_TimestampValues_are_equal()
        {
            var notLongAgo = DateTimeOffset.Now.ToUnixTimeSeconds();
            var timestamp1 = Timestamp.From(notLongAgo);
            var timestamp2 = Timestamp.From(notLongAgo);

            timestamp1.Should().Be(timestamp2);
        }
        
        [Fact]
        public void two_Timestamp_objects_with_different_TimestampValues_must_not_be_equal()
        {
            var notLongAgo1 = DateTimeOffset.Now.ToUnixTimeSeconds();
            System.Threading.Thread.Sleep(2000);
            var notLongAgo2 = DateTimeOffset.Now.ToUnixTimeSeconds();
            var timestamp1 = Timestamp.From(notLongAgo1);
            var timestamp2 = Timestamp.From(notLongAgo2);

            timestamp1.Should().NotBe(timestamp2);
        }
        
        [Fact]
        public void Timestamp_object_is_not_equal_to_null()
        {
            var notLongAgo = DateTimeOffset.Now.ToUnixTimeSeconds();
            var timestamp = Timestamp.From(notLongAgo);

            timestamp.Should().NotBe(null);
        }
    }
}