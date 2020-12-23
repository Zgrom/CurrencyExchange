namespace CurrencyExchangeDomain.DomainExceptions
{
    public sealed class ArgumentCannotBeNullOrWhitespaceException : DomainException
    {
        public ArgumentCannotBeNullOrWhitespaceException(string nameOfArgument) : 
            base(string.Format($"{nameOfArgument} cannot be null or white space.")) {}
    }
}