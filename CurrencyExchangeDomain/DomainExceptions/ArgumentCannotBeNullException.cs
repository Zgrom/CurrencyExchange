using System;

namespace CurrencyExchangeDomain.DomainExceptions
{
    public sealed class ArgumentCannotBeNullException : DomainException
    {
        public ArgumentCannotBeNullException(string nameOfArgument) : 
            base(string.Format($"{nameOfArgument} cannot be null.")) {}
    }
}