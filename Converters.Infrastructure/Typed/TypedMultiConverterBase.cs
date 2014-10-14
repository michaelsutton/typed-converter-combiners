using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Converters.Infrastructure.Utils;

namespace Converters.Infrastructure.Typed
{
    public class TypedMultiConverterBase : TypedConverterBase
    {
        #region Mismatch classes

        protected class CountMismatch : Mismatch
        {
            public readonly int ActualCount;
            public readonly int DeclaredCount;

            public CountMismatch(int actualCount, int declaredCount)
            {
                ActualCount = actualCount;
                DeclaredCount = declaredCount;
            }

            public override string GetDescription()
            {
                return string.Format(
                    "Expecting {0} values. The passed array contains {1} values.",
                    ActualCount,
                    DeclaredCount
                    );
            }
        }

        #endregion

        #region Fallbacks

        protected virtual object ConvertFallback(object[] values, Type targetType, object parameter, CultureInfo culture,
                                                 Mismatch mismatch)
        {
            throw new TypedConversionException(mismatch.GetDescription());
        }

        protected virtual object[] ConvertBackFallback(object value, Type[] targetTypes, object parameter, CultureInfo culture,
                                                       Mismatch mismatch)
        {
            throw new TypedConversionException(mismatch.GetDescription());
        }

        #endregion
    }
}
