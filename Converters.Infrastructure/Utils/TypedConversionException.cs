using System;

namespace Converters.Infrastructure.Utils
{
    public class TypedConversionException : InvalidConversionException
    {
        public TypedConversionException()
            : base("A Type mismatch was found during the current conversion flow. (conversion was declared as Typed)")
        {
        }

        public TypedConversionException(string message)
            : base(message)
        {
        }

        public TypedConversionException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
