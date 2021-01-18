using System;
using CurrencyExchangeDomain;
using CurrencyExchangeDomain.DomainExceptions;
using FluentAssertions;
using Xunit;

namespace CurrencyExchangeDomain.Tests
{
    public sealed class CurrencyExchangeTests
    {
        private readonly Symbol _baseCurrencySymbol = Symbol.From("AFN");
        private readonly Symbol _targetCurrencySymbol = Symbol.From("AED");
        private readonly Timestamp _timestampOld = Timestamp.From(123123);
        private readonly Timestamp _timestampNow = Timestamp.From(DateTimeOffset.Now.ToUnixTimeSeconds());
        private readonly Rate _rate = Rate.From(120.0);
        private readonly Amount _baseAmount = Amount.From(100.0);
        
        [Fact]
        public void creating_CurrencyExchange_object_for_valid_arguments_value_should_be_successful()
        {
            var currencyExchange = CurrencyExchange.Of(
                _baseCurrencySymbol,
                _targetCurrencySymbol,
                _timestampOld,
                _rate,
                _baseAmount);

            currencyExchange.BaseCurrency.SymbolValue.Should().Be("AFN");
            currencyExchange.TargetCurrency.SymbolValue.Should().Be("AED");
            currencyExchange.Timestamp.TimestampValue.Should().Be(123123);
            currencyExchange.Rate.RateValue.Should().Be(120.0);
            currencyExchange.BaseCurrencyAmount.AmountValue.Should().Be(100.0);
            currencyExchange.TargetCurrencyAmount.AmountValue.Should().Be(12000.0);
        }
        
        [Fact]
        public void creating_CurrencyExchange_object_for_null_BaseCurrency_value_should_fail()
        {
            Action action = () => CurrencyExchange.Of(
                null,
                _targetCurrencySymbol,
                _timestampOld,
                _rate,
                _baseAmount);

            action.Should().Throw<ArgumentCannotBeNullException>()
                .WithMessage("baseCurrency cannot be null.");
        }
        
        [Fact]
        public void creating_CurrencyExchange_object_for_null_TargetCurrency_value_should_fail()
        {
            Action action = () => CurrencyExchange.Of(
                _baseCurrencySymbol,
                null,
                _timestampOld,
                _rate,
                _baseAmount);

            action.Should().Throw<ArgumentCannotBeNullException>()
                .WithMessage("targetCurrency cannot be null.");
        }
        
        [Fact]
        public void creating_CurrencyExchange_object_for_null_Timestamp_value_should_fail()
        {
            Action action = () => CurrencyExchange.Of(
                _baseCurrencySymbol,
                _targetCurrencySymbol,
                null,
                _rate,
                _baseAmount);

            action.Should().Throw<ArgumentCannotBeNullException>()
                .WithMessage("timestamp cannot be null.");
        }
        
        [Fact]
        public void creating_CurrencyExchange_object_for_null_Rate_value_should_fail()
        {
            Action action = () => CurrencyExchange.Of(
                _baseCurrencySymbol,
                _targetCurrencySymbol,
                _timestampOld,
                null,
                _baseAmount);

            action.Should().Throw<ArgumentCannotBeNullException>()
                .WithMessage("rate cannot be null.");
        }
        
        [Fact]
        public void creating_CurrencyExchange_object_for_null_BaseAmount_value_should_fail()
        {
            Action action = () => CurrencyExchange.Of(
                _baseCurrencySymbol,
                _targetCurrencySymbol,
                _timestampOld,
                _rate,
                null);

            action.Should().Throw<ArgumentCannotBeNullException>()
                .WithMessage("baseCurrencyAmount cannot be null.");
        }
        
        [Fact]
        public void if_timestamp_is_too_old_then_method_IsDataTooOld_should_return_true()
        {
            var currencyExchange = CurrencyExchange.Of(
                _baseCurrencySymbol,
                _targetCurrencySymbol,
                _timestampOld,
                _rate,
                _baseAmount);

            currencyExchange.IsDataTooOld().Should().BeTrue();
        }
        
        [Fact]
        public void if_timestamp_is_not_too_old_then_method_IsDataTooOld_should_return_false()
        {
            var currencyExchange = CurrencyExchange.Of(
                _baseCurrencySymbol,
                _targetCurrencySymbol,
                _timestampNow,
                _rate,
                _baseAmount);

            currencyExchange.IsDataTooOld().Should().BeFalse();
        }
    }
}