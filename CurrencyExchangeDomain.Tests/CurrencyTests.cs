using System;
using CurrencyExchangeDomain;
using FluentAssertions;
using Xunit;

namespace CurrencyExchangeOffice.Tests
{
    public sealed class CurrencyTests
    {
        [Fact]
        public void creating_Currency_object_for_valid_argument_values_should_be_successful()
        {
            var symbol = Symbol.From("AFN");
            var currencyName = CurrencyName.From("Afghan Afghani");
            var currency = Currency.Of(symbol, currencyName);

            currency.Symbol.SymbolValue.Should().Be("AFN");
            currency.CurrencyName.CurrencyNameValue.Should().Be("Afghan Afghani");
        }
        
        [Fact]
        public void creating_Currency_object_for_null_Symbol_argument_value_should_fail()
        {
            var currencyName = CurrencyName.From("Afghan Afghani");
            Action action = () => Currency.Of(null,currencyName);

            action.Should().Throw<ArgumentException>()
                .WithMessage("currencySymbol cannot be null.");

        }
        
        [Fact]
        public void creating_Currency_object_for_null_currencyName_argument_value_should_fail()
        {
            var symbol = Symbol.From("AFN");
            Action action = () => Currency.Of(symbol,null);

            action.Should().Throw<ArgumentException>()
                .WithMessage("currencyName cannot be null.");

        }
        
        [Fact]
        public void Currency_objects_with_equal_symbolValues_and_currencyNameValues_are_equal()
        {
            var symbol = Symbol.From("AFN");
            var currencyName = CurrencyName.From("Afghan Afghani");

            var currency1 = Currency.Of(symbol, currencyName);
            var currency2 = Currency.Of(symbol, currencyName);

            currency1.Should().Be(currency2);
        }
        
        [Fact]
        public void Currency_objects_with_not_equal_both_argument_values_must_not_be_equal()
        {
            var symbol1 = Symbol.From("AFN");
            var currencyName1 = CurrencyName.From("Afghan Afghani");
            var symbol2 = Symbol.From("AED");
            var currencyName2 = CurrencyName.From("United Arab Emirates Dirham");

            var currency1 = Currency.Of(symbol1, currencyName1);
            var currency2 = Currency.Of(symbol1, currencyName2);
            var currency3 = Currency.Of(symbol2, currencyName2);
            var currency4 = Currency.Of(symbol2, currencyName2);

            currency1.Should().NotBe(currency2);
            currency1.Should().NotBe(currency3);
            currency1.Should().NotBe(currency4);
        }
        
        [Fact]
        public void Currency_object_is_not_equal_to_null()
        {
            var symbol = Symbol.From("AED");
            var currencyName = CurrencyName.From("United Arab Emirates Dirham");
            var currency = Currency.Of(symbol, currencyName);

            currency.Should().NotBe(null);
        }
    }
}