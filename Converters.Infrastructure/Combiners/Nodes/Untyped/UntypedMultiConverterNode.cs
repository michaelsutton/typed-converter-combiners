using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Data;
using Converters.Infrastructure.Combiners.Traversal;

namespace Converters.Infrastructure.Combiners.Nodes.Untyped
{
    internal class UntypedMultiConverterNode : UntypedConverterNodeBase
    {
        #region Fields

        private readonly IMultiValueConverter m_converter;
        private readonly List<IConverterNode> m_children;

        #endregion

        #region CTOR

        internal UntypedMultiConverterNode(IMultiValueConverter converter)
        {
            m_converter = converter;
            m_children = new List<IConverterNode>();
        }

        #endregion

        #region IConverterNode Imp

        public override ConversionFlow SetChildren(IList<IConverterNode> children)
        {
            m_children.Clear();
            m_children.AddRange(children);
            return ConversionFlow.Bidirectional;
        }

        public override object ConvertTraversal(ConvertTraversalData data)
        {
            int count = m_children.Count;
            object[] inputs = new object[count];

            for (int i = 0; i < count; i++)
            {
                inputs[i] = m_children[i].ConvertTraversal(data);
            }

            return m_converter.Convert(
                inputs,
                this == data.Root ? data.TargetType : typeof(object),
                data.Parameter,
                data.Culture
                );
        }

        public override void ConvertBackTraversal(ConvertBackTraversalData data, object value)
        {
            Type objectType = typeof (object);
            object[] outputs = m_converter.ConvertBack(
                    value,
                    m_children.Select(c => objectType).ToArray(),
                    data.Parameter,
                    data.Culture
                    );

            Debug.Assert(m_children.Count == outputs.Length);
            for (int i = 0; i < m_children.Count; i++)
            {
                m_children[i].ConvertBackTraversal(data, outputs[i]);
            }
        }

        #endregion
    }
}
