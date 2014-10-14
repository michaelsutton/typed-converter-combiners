using System;
using System.Collections.Generic;
using System.Windows.Data;
using Converters.Infrastructure.Combiners.Traversal;

namespace Converters.Infrastructure.Combiners.Nodes.Untyped
{
    internal class UntypedConverterLeaf : UntypedConverterNodeBase
    {
        #region Fields

        private readonly IValueConverter m_converter;
        private readonly int m_mappingIndex; 

        #endregion

        #region CTOR

        public UntypedConverterLeaf(IValueConverter converter, int mappingIndex)
        {
            m_converter = converter;
            m_mappingIndex = mappingIndex;
        }

        #endregion

        public override object ConvertTraversal(ConvertTraversalData data)
        {
            return m_converter.Convert(
                data.GetValue(m_mappingIndex),
                this == data.Root ? data.TargetType : typeof(object),
                data.Parameter,
                data.Culture
                );
        }

        public override void ConvertBackTraversal(ConvertBackTraversalData data, object value)
        {
            object output = m_converter.ConvertBack(
                value,
                data.GetTargetType(m_mappingIndex),
                data.Parameter,
                data.Culture
                );

            data.SetValue(output, m_mappingIndex);
        }
    }
}