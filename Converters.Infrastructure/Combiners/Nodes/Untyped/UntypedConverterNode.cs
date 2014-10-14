using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Data;
using Converters.Infrastructure.Combiners.Traversal;

namespace Converters.Infrastructure.Combiners.Nodes.Untyped
{
    internal class UntypedConverterNode : UntypedConverterNodeBase
    {
        #region Fields

        private readonly IValueConverter m_converter;
        private IConverterNode m_child;

        #endregion

        #region CTOR

        internal UntypedConverterNode(IValueConverter converter)
        {
            m_converter = converter;
        }

        #endregion

        #region IConverterNode Imp

        public override ConversionFlow SetChild(IConverterNode child)
        {
            m_child = child;
            return ConversionFlow.Bidirectional;
        }

        public override object ConvertTraversal(ConvertTraversalData data)
        {
            return m_converter.Convert(
                m_child.ConvertTraversal(data),
                this == data.Root ? data.TargetType : typeof(object),
                data.Parameter,
                data.Culture
                );
        }

        public override void ConvertBackTraversal(ConvertBackTraversalData data, object value)
        {
            object output = m_converter.ConvertBack(
                value,
                typeof(object),
                data.Parameter,
                data.Culture
                );

            m_child.ConvertBackTraversal(data, output);
        }

        #endregion
    }
}