using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Converters.Infrastructure.Combiners.Traversal
{
    internal class ConvertBackChainTraversalData : ConvertBackTraversalData
    {
        private readonly Type m_targetType;
        private object m_output; 

        public ConvertBackChainTraversalData(Type targetType, object parameter, CultureInfo culture) 
            : base(parameter, culture)
        {
            m_targetType = targetType;
        }

        internal object GetOutput()
        {
            return m_output;
        }

        internal override Type[] GetTargetTypes(IEnumerable<int> mapping)
        {
            throw new NotSupportedException();
        }

        internal override Type GetTargetType(int index)
        {
            Debug.Assert(index == 0);
            return m_targetType;
        }

        internal override void SetValues(object[] values, IEnumerable<int> mapping)
        {
            throw new NotSupportedException();
        }

        internal override void SetValue(object value, int index)
        {
            Debug.Assert(index == 0);
            m_output = value;
        }
    }
}