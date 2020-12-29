using System.Collections.Generic;
using System.Linq;
using CurrencyExchangeDomain;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;

namespace MongoAdapter.DTO
{
    public sealed class LatestRatesDto
    {
        public string Id { get; set; }
        public long Timestamp { get; set; }
        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        public Dictionary<CurrencyAsPropertyDto, double> Rates { get; set; }

        public LatestRates ToDomain()
        {
            var timestamp = CurrencyExchangeDomain.Timestamp.From(Timestamp);
            var rates = new Dictionary<Currency,Rate>();
            foreach (var rate in Rates)
            {
                var currencySymbol = Symbol.From(rate.Key.Symbol);
                var currencyName = CurrencyName.From(rate.Key.CurrencyName);
                var currency = Currency.Of(currencySymbol, currencyName);
                var currencyExchangeRate = Rate.From(rate.Value);
                rates.Add(currency,currencyExchangeRate);
            }
            
            return LatestRates.Of(timestamp, rates);
        }
    }
}