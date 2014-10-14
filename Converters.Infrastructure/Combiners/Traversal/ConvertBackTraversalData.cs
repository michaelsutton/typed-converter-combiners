using System;
using System.Collections.Generic;
using System.Globalization;

namespace Converters.Infrastructure.Combiners.Traversal
{
    internal abstract class ConvertBackTraversalData
    {
        internal readonly object Parameter;
        internal readonly CultureInfo Culture;

        internal ConvertBackTraversalData(object parameter, CultureInfo culture)
        {
            Parameter = parameter;
            Culture = culture;
        }

        internal abstract Type[] GetTargetTypes(IEnumerable<int> mapping);
        internal abstract Type GetTargetType(int index);

        internal abstract void SetValues(object[] values, IEnumerable<int> mapping);
        internal abstract void SetValue(object value, int index);
    }
}