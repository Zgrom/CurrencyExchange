using System;

namespace Ports.RepositoryExceptions
{
    public sealed class CurrencySymbolNotAvailableException : Exception
    {
        public CurrencySymbolNotAvailableException(string currencySymbol) : 
            base($"Currency with symbol {currencySymbol} is not available."){}
    }
}