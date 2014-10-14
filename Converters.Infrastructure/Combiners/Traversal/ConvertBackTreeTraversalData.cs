using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Converters.Infrastructure.Combiners.Traversal
{
    internal class ConvertBackTreeTraversalData : ConvertBackTraversalData
    {
        private readonly Type[] m_targetTypes;
        private readonly object[] m_outputs; 

        internal ConvertBackTreeTraversalData(Type[] targetTypes, object parameter, CultureInfo culture)
            : base(parameter, culture)
        {
            m_targetTypes = targetTypes;
            m_outputs = new object[targetTypes.Length];
        }

        internal object[] GetOutputs()
        {
            return m_outputs;
        }

        internal override Type[] GetTargetTypes(IEnumerable<int> mapping)
        {
            return mapping.Select(i => m_targetTypes[i]).ToArray();
        }

        internal override Type GetTargetType(int index)
        {
            return m_targetTypes[index];
        }

        internal override void SetValues(object[] values, IEnumerable<int> mapping)
        {
            foreach (var tuple in GetTuples(values, mapping))
            {
                m_outputs[tuple.Item1] = tuple.Item2;
            }
        }

        internal override void SetValue(object value, int index)
        {
            m_outputs[index] = value;
        }

        private static IEnumerable<Tuple<int, object>> GetTuples(object[] values, IEnumerable<int> mapping)
        {
            int i = 0;
            return mapping.Select(index => new Tuple<int, object>(index, values[i++]));
        }
    }
}