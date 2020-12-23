using System;

namespace CurrencyExchangeDomain.DomainExceptions
{
    public abstract class DomainException : Exception
    {
        protected DomainException(string message) : base(message){}
    }
}