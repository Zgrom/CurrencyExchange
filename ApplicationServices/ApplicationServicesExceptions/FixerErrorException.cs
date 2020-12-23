using System;

namespace ApplicationServices.ApplicationServicesExceptions
{
    public class FixerErrorException : Exception
    {
        public FixerErrorException(int code, string message) : 
            base(string.Format($"code: {code}, info {message}.")) {}
    }
}