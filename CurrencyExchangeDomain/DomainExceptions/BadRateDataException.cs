namespace CurrencyExchangeDomain.DomainExceptions
{
    public sealed class BadRateDataException : DomainException
    {
        public BadRateDataException(string argument) : 
            base(string.Format($"Bad rate date for currency {argument}")) {}
    }
}