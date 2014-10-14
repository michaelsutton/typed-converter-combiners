using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using Converters.Infrastructure.Combiners.Nodes.Untyped;

namespace Converters.Infrastructure.Combiners.Traversal
{
    internal class ConvertChainTraversalData : ConvertTraversalData
    {
        private readonly object m_value;

        internal ConvertChainTraversalData(object value, Type targetType, object parameter, CultureInfo culture, IConverterNode root)
            : base(targetType, parameter, culture, root)
        {
            m_value = value;
        }

        internal override object[] GetValues(IEnumerable<int> mapping)
        {
            throw new NotSupportedException();
        }

        internal override object GetValue(int index)
        {
            Debug.Assert(index == 0);
            return m_value;
        }
    }
}
