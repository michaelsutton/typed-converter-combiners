using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Converters.Infrastructure.Combiners.Nodes.Untyped;
using Converters.Infrastructure.Combiners.Traversal;
using Converters.Infrastructure.Typed.Many;

namespace Converters.Infrastructure.Combiners.Nodes.Typed.Many
{
    internal class TypedMultiConverterNode<TSource, TTarget> : TypedConverterNodeBase<TTarget>
    {
        #region Fields

        private readonly TypedMultiValueConverter<TSource, TTarget> m_converter;
        private readonly List<IConverterNode> m_children;

        #endregion

        #region CTOR

        public TypedMultiConverterNode(TypedMultiValueConverter<TSource, TTarget> converter)
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

            return m_children.Aggregate(
                ConversionFlow.Bidirectional, 
                (supports, child) => supports & GetSupportedConversionFlows<TSource>(child)
                );
        }

        public override TTarget ConvertTraversal(ConvertTraversalData data)
        {
            int count = m_children.Count;
            TSource[] inputs = new TSource[count];

            for (int i = 0; i < count; i++)
            {
                inputs[i] = ((IConvertNode<TSource>) m_children[i]).ConvertTraversal(data);
            }

            return m_converter.Convert(
                inputs,
                this == data.Root ? data.TargetType : typeof(TTarget),
                data.Parameter,
                data.Culture
                );
        }

        public override void ConvertBackTraversal(ConvertBackTraversalData data, TTarget value)
        {
            Type sourceType = typeof(TSource);
            TSource[] outputs = m_converter.ConvertBack(
                    value,
                    m_children.Select(c => sourceType).ToArray(),
                    data.Parameter,
                    data.Culture
                    );

            Debug.Assert(m_children.Count == outputs.Length);
            for (int i = 0; i < m_children.Count; i++)
            {
                ((IConvertBackNode<TSource>) m_children[i]).ConvertBackTraversal(data, outputs[i]);
            }
        }

        #endregion
    }
}