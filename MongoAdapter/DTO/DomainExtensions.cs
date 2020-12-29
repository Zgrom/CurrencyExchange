using System;
using System.Collections.Generic;
using CurrencyExchangeDomain;

namespace MongoAdapter.DTO
{
    public static class DomainExtensions
    {
        public static CurrencyExchangeDto ToDto(this CurrencyExchange currencyExchange)
            => new CurrencyExchangeDto
            {
                Id = Guid.NewGuid().ToString(),
                BaseCurrency = new CurrencyAsPropertyDto
                {
                    Symbol = currencyExchange.BaseCurrency.Symbol.SymbolValue,
                    CurrencyName = currencyExchange.BaseCurrency.CurrencyName.CurrencyNameValue
                },
                TargetCurrency = new CurrencyAsPropertyDto
                {
                    Symbol = currencyExchange.TargetCurrency.Symbol.SymbolValue,
                    CurrencyName = currencyExchange.TargetCurrency.CurrencyName.CurrencyNameValue
                },
                Timestamp = currencyExchange.Timestamp.TimestampValue,
                Rate = currencyExchange.Rate.RateValue,
            };

        public static LatestRatesDto ToDto(this LatestRates latestRates)
        {
            var timestamp = latestRates.Timestamp.TimestampValue;
            var rates = new Dictionary<CurrencyAsPropertyDto,double>();
            foreach (var rate in latestRates.Rates)
            {
                var currencySymbol = rate.Key.Symbol.SymbolValue;
                var currencyName = rate.Key.CurrencyName.CurrencyNameValue;
                var currency = new CurrencyAsPropertyDto
                {
                    Symbol = currencySymbol,
                    CurrencyName = currencyName
                };
                var currencyRate = rate.Value.RateValue;
                
                rates.Add(currency,currencyRate);
            }
            return new LatestRatesDto
            {
                Timestamp = timestamp,
                Rates = rates
            };
        }

        public static CurrencyDto ToDto(this Currency currency)
            => new CurrencyDto
            {
                Id = Guid.NewGuid().ToString(),
                Symbol = currency.Symbol.SymbolValue,
                CurrencyName = currency.CurrencyName.CurrencyNameValue
            };

    }
}