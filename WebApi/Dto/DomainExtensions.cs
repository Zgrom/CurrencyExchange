using CurrencyExchangeDomain;
namespace WebApi.Dto
{
    public static class DomainExtensions
    {
        public static CurrencyDto ToDtoWebApi(this Currency currency)
            => new CurrencyDto
            {
                CurrencySymbol = currency.Symbol.SymbolValue,
                CurrencyName = currency.CurrencyName.CurrencyNameValue
            };
    }
}