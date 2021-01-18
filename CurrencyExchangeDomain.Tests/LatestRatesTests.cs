using System;
using System.Collections.Generic;
using CurrencyExchangeDomain.DomainExceptions;
using FluentAssertions;
using Xunit;

namespace CurrencyExchangeDomain.Tests
{
    public sealed class LatestRatesTests
    {
        private readonly Timestamp _timestamp = Timestamp.From(123123);
        private readonly Dictionary<Symbol,Rate> _rates = new Dictionary<Symbol, Rate>()
        {
            {Symbol.From("AFN"), Rate.From(120.0)},
            {Symbol.From("AED"), Rate.From(12.0)}
        };

        [Fact]
        public void creating_LatestRates_object_for_valid_arguments_value_should_be_successful()
        {
            var latestRates = LatestRates.Of(_timestamp, _rates);

            latestRates.Timestamp.TimestampValue.Should().Be(123123);
            latestRates
                .Rates[Symbol.From("AFN")]
                .RateValue
                .Should().Be(120.0);
            latestRates.Rates.Count.Should().Be(2);
        }

        [Fact]
        public void creating_LatestRates_object_for_null_Timestamp_value_should_fail()
        {
            Action action = () => LatestRates.Of(null, _rates);

            action.Should().Throw<ArgumentCannotBeNullException>()
                .WithMessage("timestamp cannot be null.");
        }
        
        [Fact]
        public void creating_LatestRates_object_for_null_Rates_value_should_fail()
        {
            Action action = () => LatestRates.Of(_timestamp, null);

            action.Should().Throw<ArgumentCannotBeNullException>()
                .WithMessage("rates cannot be null.");
        }

        [Fact]
        public void GetRateFor_method_should_return_accurate_value()
        {
            var latestRates = LatestRates.Of(_timestamp, _rates);
            var currencyAfghani = Symbol.From("AFN");
            var currencyDirham = Symbol.From("AED");

            latestRates.GetRateFor(currencyAfghani,currencyDirham)
                .RateValue.Should().Be(0.1);
            latestRates.GetRateFor(currencyDirham, currencyAfghani)
                .RateValue.Should().Be(10);
            latestRates.GetRateFor(currencyAfghani, currencyAfghani)
                .RateValue.Should().Be(1);
        }

        [Fact]
        public void if_timestamp_is_too_old_then_method_IsTooOld_should_return_true()
        {
            var latestRates = LatestRates.Of(_timestamp, _rates);

            latestRates.Timestamp.IsTooOld().Should().BeTrue();
        }

        [Fact]
        public void if_timestamp_is_not_too_old_then_method_IsTooOld_should_return_false()
        {
            var latestRates = LatestRates.Of(Timestamp.From(DateTimeOffset.Now.ToUnixTimeSeconds()), _rates);

            latestRates.Timestamp.IsTooOld().Should().BeFalse();
        }

        [Fact]
        public void LatestRates_objects_with_equal_Timestamp_values_are_equal()
        {
            var latestRates1 = LatestRates.Of(_timestamp,_rates);
            var latestRates2 = LatestRates.Of(_timestamp,_rates);

            latestRates1.Should().Be(latestRates2);
        }
        
        [Fact]
        public void two_LatestRates_objects_with_different_Timestamp_values_must_not_be_equal()
        {
            var timestampNow = Timestamp.From(DateTimeOffset.Now.ToUnixTimeSeconds());
            var latestRates1 = LatestRates.Of(_timestamp,_rates);
            var latestRates2 = LatestRates.Of(timestampNow,_rates);;

            latestRates1.Should().NotBe(latestRates2);
        }
        
        [Fact]
        public void LatestRates_object_is_not_equal_to_null()
        {
            var latestRates = LatestRates.Of(_timestamp,_rates);

            latestRates.Should().NotBe(null);
        }
    }
}