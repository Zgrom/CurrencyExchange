using System;

namespace Ports.RepositoryExceptions
{
    public sealed class NoValidCurrencyExchangeRateException : Exception
    {
        public NoValidCurrencyExchangeRateException() : 
            base("No valid currency exchange rate for given currencies."){}
    }
}