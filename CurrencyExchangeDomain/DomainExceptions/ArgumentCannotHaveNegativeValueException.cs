namespace CurrencyExchangeDomain.DomainExceptions
{
    public sealed class ArgumentCannotHaveNegativeValueException : DomainException
    {
        public ArgumentCannotHaveNegativeValueException(string nameOfArgument) : 
            base(string.Format($"{nameOfArgument} cannot have negative value.")) {}
    }
}