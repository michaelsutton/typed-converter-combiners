using System;
using System.Collections.Generic;
using Converters.Infrastructure.Combiners.Nodes.Untyped;
using Converters.Infrastructure.Combiners.Traversal;
using Converters.Infrastructure.Typed.Double;
using Converters.Infrastructure.Typed.Single;

namespace Converters.Infrastructure.Combiners.Nodes.Typed.Double
{
    internal class TypedDoubleConverterNode<TSource1, TSource2, TTarget> : TypedConverterNodeBase<TTarget>
    {
        #region Fields

        private IConverterNode m_child1;
        private IConverterNode m_child2;
        private readonly TypedDoubleValueConverter<TSource1, TSource2, TTarget> m_converter;

        #endregion

        #region CTOR

        public TypedDoubleConverterNode(TypedDoubleValueConverter<TSource1, TSource2, TTarget> converter)
        {
            m_converter = converter;
        }

        #endregion

        #region IConverterNode Imp

        public override ConversionFlow SetChildren(IList<IConverterNode> children)
        {
            if(children.Count != 2)
                throw new InvalidOperationException(
                    string.Format(
                        "The TypedDoubleValueConverter has only 2 inputs/outputs, but was set with {0} children", 
                        children.Count
                        )
                    );

            m_child1 = children[0];
            m_child2 = children[1];

            return GetSupportedConversionFlows<TSource1>(m_child1) &
                   GetSupportedConversionFlows<TSource2>(m_child2);
        }

        public override TTarget ConvertTraversal(ConvertTraversalData data)
        {
            TSource1 value1 = ((IConvertNode<TSource1>)m_child1).ConvertTraversal(data);
            TSource2 value2 = ((IConvertNode<TSource2>)m_child2).ConvertTraversal(data);

            return m_converter.Convert(
                value1, 
                value2,
                this == data.Root ? data.TargetType : typeof(TTarget),
                data.Parameter,
                data.Culture
                );
        }

        public override void ConvertBackTraversal(ConvertBackTraversalData data, TTarget value)
        {
            var output = m_converter.ConvertBack(
                value, 
                new Type[]{ typeof (TSource1), typeof(TSource2)}, 
                data.Parameter, 
                data.Culture
                );

            ((IConvertBackNode<TSource1>) m_child1).ConvertBackTraversal(
                data,
                output.Item1
                );

            ((IConvertBackNode<TSource2>)m_child2).ConvertBackTraversal(
                data,
                output.Item2
                );
        }

        #endregion
    }
}