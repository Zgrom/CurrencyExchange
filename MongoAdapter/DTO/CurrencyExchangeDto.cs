
namespace MongoAdapter.DTO
{
    public sealed class CurrencyExchangeDto
    {
        public string Id { get; set; }
        public string BaseCurrency { get; set; }
        public string TargetCurrency { get; set; }
        public long Timestamp { get; set; }
        public double Rate { get; set; }
    }
}