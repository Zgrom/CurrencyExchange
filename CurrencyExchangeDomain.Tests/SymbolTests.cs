using System;
using CurrencyExchangeDomain.Tests;
using FluentAssertions;
using Xunit;

namespace CurrencyExchangeDomain.Tests.Tests
{
    public sealed class SymbolTests
    {
        [Fact]
        public void creating_Symbol_object_for_valid_argument_value_should_be_successful()
        {
            var symbol = Symbol.From("RSD");

            symbol.SymbolValue.Should().Be("RSD");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void creating_Symbol_object_for_null_or_whitespace_argument_value_should_fail(string input)
        {
            Action action = () => Symbol.From(input);

            action.Should().Throw<ArgumentException>()
                .WithMessage("currencySymbol cannot be null or white space.");

        }
        
        [Theory]
        [InlineData("RS")]
        [InlineData("RSDIN")]
        public void creating_Symbol_object_for_nonnull_invalid_argument_value_should_fail(string input)
        {
            Action action = () => Symbol.From(input);

            action.Should().Throw<ArgumentException>()
                .WithMessage("currencySymbol must have length of exactly 3.");
        }
        
        [Fact]
        public void symbol_objects_with_equal_symbolValues_are_equal()
        {
            var symbol1 = Symbol.From("RSD");
            var symbol2 = Symbol.From("RSD");

            symbol1.Should().Be(symbol2);
        }
        
        [Fact]
        public void two_different_symbol_objects_must_not_be_equal()
        {
            var symbol1 = Symbol.From("RSD");
            var symbol2 = Symbol.From("USD");

            symbol1.Should().NotBe(symbol2);
        }
        
        [Fact]
        public void symbol_object_is_not_equal_to_null()
        {
            var symbol = Symbol.From("RSD");

            symbol.Should().NotBe(null);
        }
    }
}