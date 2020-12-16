using System;
using System.Collections.Generic;
using CurrencyExchangeDomain;
using FluentAssertions;
using Xunit;

namespace CurrencyExchangeOffice.Tests
{
    public sealed class CurrencyExchangeTests
    {
        private readonly Currency _baseCurrency = Currency.Of(
            Symbol.From("AFN"),
            CurrencyName.From("Afghan Afghani"));
        private readonly Currency _targetCurrency = Currency.Of(
            Symbol.From("AED"),
            CurrencyName.From("United Arab Emirates Dirham"));
        private readonly Timestamp _timestampOld = Timestamp.From(123123);
        private readonly Timestamp _timestampNow = Timestamp.From(DateTimeOffset.Now.ToUnixTimeSeconds());
        private readonly Rate _rate = Rate.From(120.0);
        private readonly Amount _baseAmount = Amount.From(100.0);
        
        [Fact]
        public void creating_CurrencyExchange_object_for_valid_arguments_value_should_be_successful()
        {
            var currencyExchange = CurrencyExchange.Of(
                _baseCurrency,
                _targetCurrency,
                _timestampOld,
                _rate,
                _baseAmount);

            currencyExchange.BaseCurrency.Symbol.SymbolValue.Should().Be("AFN");
            currencyExchange.BaseCurrency.CurrencyName.CurrencyNameValue.Should().Be("Afghan Afghani");
            currencyExchange.TargetCurrency.Symbol.SymbolValue.Should().Be("AED");
            currencyExchange.TargetCurrency.CurrencyName.CurrencyNameValue.Should().Be("United Arab Emirates Dirham");
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
                _targetCurrency,
                _timestampOld,
                _rate,
                _baseAmount);

            action.Should().Throw<ArgumentException>()
                .WithMessage("baseCurrency cannot be null.");
        }
        
        [Fact]
        public void creating_CurrencyExchange_object_for_null_TargetCurrency_value_should_fail()
        {
            Action action = () => CurrencyExchange.Of(
                _baseCurrency,
                null,
                _timestampOld,
                _rate,
                _baseAmount);

            action.Should().Throw<ArgumentException>()
                .WithMessage("targetCurrency cannot be null.");
        }
        
        [Fact]
        public void creating_CurrencyExchange_object_for_null_Timestamp_value_should_fail()
        {
            Action action = () => CurrencyExchange.Of(
                _baseCurrency,
                _targetCurrency,
                null,
                _rate,
                _baseAmount);

            action.Should().Throw<ArgumentException>()
                .WithMessage("timestamp cannot be null.");
        }
        
        [Fact]
        public void creating_CurrencyExchange_object_for_null_Rate_value_should_fail()
        {
            Action action = () => CurrencyExchange.Of(
                _baseCurrency,
                _targetCurrency,
                _timestampOld,
                null,
                _baseAmount);

            action.Should().Throw<ArgumentException>()
                .WithMessage("rate cannot be null.");
        }
        
        [Fact]
        public void creating_CurrencyExchange_object_for_null_BaseAmount_value_should_fail()
        {
            Action action = () => CurrencyExchange.Of(
                _baseCurrency,
                _targetCurrency,
                _timestampOld,
                _rate,
                null);

            action.Should().Throw<ArgumentException>()
                .WithMessage("baseCurrencyAmount cannot be null.");
        }
        
        [Fact]
        public void if_timestamp_is_too_old_then_method_IsDataTooOld_should_return_true()
        {
            var currencyExchange = CurrencyExchange.Of(
                _baseCurrency,
                _targetCurrency,
                _timestampOld,
                _rate,
                _baseAmount);

            currencyExchange.IsDataTooOld().Should().BeTrue();
        }
        
        [Fact]
        public void if_timestamp_is_not_too_old_then_method_IsDataTooOld_should_return_false()
        {
            var currencyExchange = CurrencyExchange.Of(
                _baseCurrency,
                _targetCurrency,
                _timestampNow,
                _rate,
                _baseAmount);

            currencyExchange.IsDataTooOld().Should().BeFalse();
        }
    }
}