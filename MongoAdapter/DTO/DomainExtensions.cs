using System;
using CurrencyExchangeDomain;

namespace MongoAdapter.DTO
{
    public static class DomainExtensions
    {
        public static CurrencyExchangeDto ToDto(this CurrencyExchange currencyExchange)
            => new CurrencyExchangeDto
            {
                BaseCurrency = new CurrencyDto
                {
                    Symbol = currencyExchange.BaseCurrency.Symbol.SymbolValue,
                    CurrencyName = currencyExchange.BaseCurrency.CurrencyName.CurrencyNameValue
                },
                TargetCurrency = new CurrencyDto
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