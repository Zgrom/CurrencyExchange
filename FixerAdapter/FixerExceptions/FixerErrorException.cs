using System;

namespace FixerAdapter.FixerExceptions
{
    public class FixerErrorException : Exception
    {
        public FixerErrorException(int code, string message) : 
            base(string.Format($"Fixer.io error code: {code}, type: {message}.")) {}
    }
}