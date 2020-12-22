using CurrencyExchangeDomain;

namespace MongoAdapter.DTO
{
    public sealed class CurrencyDto
    {
        public string Id { get; set; }
        public string Symbol { get; set; }
        public string CurrencyName { get; set; }

        public Currency ToDomain()
        {
            return Currency.Of(
                CurrencyExchangeDomain.Symbol.From(Symbol),
                CurrencyExchangeDomain.CurrencyName.From(CurrencyName));
        }
    }
}