using System;
using CurrencyExchangeDomain;
using FluentAssertions;
using Xunit;

namespace CurrencyExchangeOffice.Tests
{
    public sealed class RateTests
    {
        [Fact]
        public void creating_Rate_object_for_valid_argument_value_should_be_successful()
        {
            var rate = Rate.From(1.12);

            rate.RateValue.Should().Be(1.12);
        }

        [Fact]
        public void creating_Rate_object_for_invalid_argument_value_should_fail()
        {
            Action action = () => Rate.From(-3.57);

            action.Should().Throw<ArgumentException>()
                .WithMessage("rate cannot have negative value.");
        }
        
        [Fact]
        public void Rate_objects_with_equal_rateValues_are_equal()
        {
            var rate1 = Rate.From(1.12);
            var rate2 = Rate.From(1.12);

            rate1.Should().Be(rate2);
        }
        
        [Fact]
        public void two_Rate_objects_with_different_RateValues_must_not_be_equal()
        {
            var rate1 = Rate.From(1.12);
            var rate2 = Rate.From(1.12001);

            rate1.Should().NotBe(rate2);
        }
        
        [Fact]
        public void Rate_object_is_not_equal_to_null()
        {
            var rate = Rate.From(1.12);

            rate.Should().NotBe(null);
        }
    }
}