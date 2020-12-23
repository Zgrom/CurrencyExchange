using System;

namespace ApplicationServices.ApplicationServicesExceptions
{
    public class FixerErrorException : Exception
    {
        public FixerErrorException(int code, string message) : 
            base(string.Format($"Fixer.io error code: {code}, type: {message}.")) {}
    }
}