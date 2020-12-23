namespace CurrencyExchangeDomain.DomainExceptions
{
    public sealed class ArgumentMustHaveExactLengthValueException : DomainException
    {
        public ArgumentMustHaveExactLengthValueException(string nameOfArgument) : 
            base(string.Format($"{nameOfArgument} must have length of exactly 3.")) {}
    }
}