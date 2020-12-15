using System;
using CurrencyExchangeDomain;
using FluentAssertions;
using Xunit;

namespace CurrencyExchangeOffice.Tests
{
    public sealed class AmountTests
    {
        [Fact]
        public void creating_Amount_object_for_valid_argument_value_should_be_successful()
        {
            var amount = Amount.From(112.12);

            amount.AmountValue.Should().Be(112.12);
        }

        [Fact]
        public void creating_Amount_object_for_invalid_argument_value_should_fail()
        {
            Action action = () => Amount.From(-300.57);

            action.Should().Throw<ArgumentException>()
                .WithMessage("amount cannot have negative value.");
        }
        
        [Fact]
        public void Amount_objects_with_equal_AmountValues_are_equal()
        {
            var amount1 = Amount.From(1.12);
            var amount2 = Amount.From(1.12);

            amount1.Should().Be(amount2);
        }
        
        [Fact]
        public void two_Amount_objects_with_different_AmountValues_must_not_be_equal()
        {
            var amount1 = Amount.From(112.12);
            var amount2 = Amount.From(113.12);

            amount1.Should().NotBe(amount2);
        }
        
        [Fact]
        public void Amount_object_is_not_equal_to_null()
        {
            var amount = Amount.From(1.12);

            amount.Should().NotBe(null);
        }
    }
}