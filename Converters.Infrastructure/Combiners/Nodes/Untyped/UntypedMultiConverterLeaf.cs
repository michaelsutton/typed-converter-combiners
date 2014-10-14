using System;
using System.Collections.Generic;
using System.Windows.Data;
using Converters.Infrastructure.Combiners.Traversal;

namespace Converters.Infrastructure.Combiners.Nodes.Untyped
{
    internal class UntypedMultiConverterLeaf : UntypedConverterNodeBase
    {
        #region Fields

        private readonly IMultiValueConverter m_converter;
        private readonly IEnumerable<int> m_mapping; 

        #endregion

        #region CTOR

        public UntypedMultiConverterLeaf(IMultiValueConverter converter, IEnumerable<int> mapping)
        {
            m_converter = converter;
            m_mapping = mapping;
        }

        #endregion

        public override object ConvertTraversal(ConvertTraversalData data)
        {
            return m_converter.Convert(
                data.GetValues(m_mapping),
                this == data.Root ? data.TargetType : typeof(object),
                data.Parameter,
                data.Culture
                );
        }

        public override void ConvertBackTraversal(ConvertBackTraversalData data, object value)
        {
            object[] outputs = m_converter.ConvertBack(
                value,
                data.GetTargetTypes(m_mapping),
                data.Parameter,
                data.Culture
                );

            data.SetValues(outputs, m_mapping);
        }
    }
}
