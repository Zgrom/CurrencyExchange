using System;
using CurrencyExchangeDomain;

namespace MongoAdapter.DTO
{
    public static class DomainExtensions
    {
        public static CurrencyExchangeDto ToDto(this CurrencyExchange currencyExchange)
            => new CurrencyExchangeDto
            {
                Id = Guid.NewGuid().ToString(),
                BaseCurrency = new CurrencyInCurrencyExchangeDto
                {
                    Symbol = currencyExchange.BaseCurrency.Symbol.SymbolValue,
                    CurrencyName = currencyExchange.BaseCurrency.CurrencyName.CurrencyNameValue
                },
                TargetCurrency = new CurrencyInCurrencyExchangeDto
                {
                    Symbol = currencyExchange.TargetCurrency.Symbol.SymbolValue,
                    CurrencyName = currencyExchange.TargetCurrency.CurrencyName.CurrencyNameValue
                },
                Timestamp = currencyExchange.Timestamp.TimestampValue,
                Rate = currencyExchange.Rate.RateValue,
            };

        public static CurrencyDto ToDto(this Currency currency)
            => new CurrencyDto
            {
                Id = Guid.NewGuid().ToString(),
                Symbol = currency.Symbol.SymbolValue,
                CurrencyName = currency.CurrencyName.CurrencyNameValue
            };

    }
}