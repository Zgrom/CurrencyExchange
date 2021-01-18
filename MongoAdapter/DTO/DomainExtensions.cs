using System;
using System.Collections.Generic;
using CurrencyExchangeDomain;

namespace MongoAdapter.DTO
{
    public static class DomainExtensions
    {
        public static LatestRatesDto ToDto(this LatestRates latestRates)
        {
            var id = Guid.NewGuid().ToString();
            var timestamp = latestRates.Timestamp.TimestampValue;
            var rates = new Dictionary<string,double>();
            foreach (var rate in latestRates.Rates)
            {
                var currencySymbol = rate.Key.SymbolValue;
                var currencyRate = rate.Value.RateValue;
                
                rates.Add(currencySymbol,currencyRate);
            }
            return new LatestRatesDto
            {
                Id = id,
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