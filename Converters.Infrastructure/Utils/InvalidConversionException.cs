using System;

namespace Converters.Infrastructure.Utils
{
    public class InvalidConversionException : Exception
    {
        public InvalidConversionException()
        {
            
        }

        public InvalidConversionException(string message)
            : base(message)
        {
            
        }

        public InvalidConversionException(string message, Exception innerException)
            : base(message, innerException)
        {
            
        }
    }
}