using System.Collections.Generic;
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
        public Dictionary<string, double> Rates { get; set; }

        public LatestRates ToDomain()
        {
            var timestamp = CurrencyExchangeDomain.Timestamp.From(Timestamp);
            var rates = new Dictionary<Symbol,Rate>();
            foreach (var rate in Rates)
            {
                var currencySymbol = Symbol.From(rate.Key);
                var currencyExchangeRate = Rate.From(rate.Value);
                rates.Add(currencySymbol,currencyExchangeRate);
            }
            
            return LatestRates.Of(timestamp, rates);
        }
    }
}