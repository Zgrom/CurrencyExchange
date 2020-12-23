namespace WebApi.Dto
{
    public sealed class CurrencyExchangeDto
    {
        public string BaseCurrencySymbol { get; set; }
        public string TargetCurrencySymbol { get; set; }
        public double BaseCurrencyAmount { get; set; }
    }
}