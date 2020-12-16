using System;
using CurrencyExchangeDomain;
using FluentAssertions;
using Xunit;

namespace CurrencyExchangeOffice.Tests
{
    public sealed class CurrencyNameTests
    {
        [Fact]
        public void creating_CurrencyName_object_for_valid_argument_value_should_be_successful()
        {
            var currencyName = CurrencyName.From("Afghan Afghani");

            currencyName.CurrencyNameValue.Should().Be("Afghan Afghani");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void creating_CurrencyName_object_for_null_or_whitespace_argument_value_should_fail(string input)
        {
            Action action = () => CurrencyName.From(input);

            action.Should().Throw<ArgumentException>()
                .WithMessage("currencyName cannot be null or white space.");

        }
        
        [Fact]
        public void CurrencyName_objects_with_equal_CurrencyNameValues_are_equal()
        {
            var currencyName1 = CurrencyName.From("Afghan Afghani");
            var currencyName2 = CurrencyName.From("Afghan Afghani");

            currencyName1.Should().Be(currencyName2);
        }
        
        [Fact]
        public void two_different_CurrencyName_objects_must_not_be_equal()
        {
            var currencyName1 = CurrencyName.From("Afghan Afghani");
            var currencyName2 = CurrencyName.From("United Arab Emirates Dirham");

            currencyName1.Should().NotBe(currencyName2);
        }
        
        [Fact]
        public void CurrencyName_object_is_not_equal_to_null()
        {
            var currencyName = CurrencyName.From("United Arab Emirates Dirham");

            currencyName.Should().NotBe(null);
        }
    }
}