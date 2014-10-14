using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Converters.Infrastructure.Combiners.Nodes.Untyped;

namespace Converters.Infrastructure.Combiners.Traversal
{
    internal class ConvertTreeTraversalData : ConvertTraversalData
    {
        private readonly object[] m_values;

        internal ConvertTreeTraversalData(object[] values, Type targetType, object parameter, CultureInfo culture, IConverterNode root)
            : base(targetType, parameter, culture, root)
        {
            m_values = values;
        }

        internal override object[] GetValues(IEnumerable<int> mapping)
        {
            return mapping.Select(i => m_values[i]).ToArray();
        }

        internal override object GetValue(int index)
        {
            return m_values[index];
        }
    }
}