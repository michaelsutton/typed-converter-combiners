using System;
using System.Collections.Generic;
using System.Globalization;
using Converters.Infrastructure.Combiners.Nodes.Untyped;

namespace Converters.Infrastructure.Combiners.Traversal
{
    internal abstract class ConvertTraversalData
    {
        internal readonly Type TargetType;
        internal readonly object Parameter;
        internal readonly CultureInfo Culture;
        internal readonly IConverterNode Root;

        internal ConvertTraversalData(Type targetType, object parameter, CultureInfo culture, IConverterNode root)
        {
            TargetType = targetType;
            Parameter = parameter;
            Culture = culture;

            Root = root;
        }

        internal abstract object[] GetValues(IEnumerable<int> mapping);
        internal abstract object GetValue(int index);
    }
}