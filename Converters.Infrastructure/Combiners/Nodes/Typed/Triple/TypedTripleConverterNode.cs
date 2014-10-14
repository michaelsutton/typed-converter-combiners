using System;
using System.Collections.Generic;
using Converters.Infrastructure.Combiners.Nodes.Untyped;
using Converters.Infrastructure.Combiners.Traversal;
using Converters.Infrastructure.Typed.Triple;

namespace Converters.Infrastructure.Combiners.Nodes.Typed.Triple
{
    internal class TypedTripleConverterNode<TSource1, TSource2, TSource3, TTarget> : TypedConverterNodeBase<TTarget>
    {
        #region Fields

        private IConverterNode m_child1;
        private IConverterNode m_child2;
        private IConverterNode m_child3;
        private readonly TypedTripleValueConverter<TSource1, TSource2, TSource3, TTarget> m_converter;

        #endregion

        #region CTOR

        public TypedTripleConverterNode(TypedTripleValueConverter<TSource1, TSource2, TSource3, TTarget> converter)
        {
            m_converter = converter;
        }

        #endregion

        #region IConverterNode Imp

        public override ConversionFlow SetChildren(IList<IConverterNode> children)
        {
            if(children.Count != 3)
                throw new InvalidOperationException(
                    string.Format(
                        "The TypedTripleValueConverter has only 3 inputs/outputs, but was set with {0} children", 
                        children.Count
                        )
                    );

            m_child1 = children[0];
            m_child2 = children[1];
            m_child3 = children[2];

            return GetSupportedConversionFlows<TSource1>(m_child1) &
                   GetSupportedConversionFlows<TSource2>(m_child2) &
                   GetSupportedConversionFlows<TSource3>(m_child3);
        }

        public override TTarget ConvertTraversal(ConvertTraversalData data)
        {
            TSource1 value1 = ((IConvertNode<TSource1>)m_child1).ConvertTraversal(data);
            TSource2 value2 = ((IConvertNode<TSource2>)m_child2).ConvertTraversal(data);
            TSource3 value3 = ((IConvertNode<TSource3>)m_child3).ConvertTraversal(data);

            return m_converter.Convert(
                value1,
                value2,
                value3,
                this == data.Root ? data.TargetType : typeof(TTarget),
                data.Parameter,
                data.Culture
                );
        }

        public override void ConvertBackTraversal(ConvertBackTraversalData data, TTarget value)
        {
            var output = m_converter.ConvertBack(
                value, 
                new Type[]{ typeof (TSource1), typeof(TSource2), typeof(TSource3)}, 
                data.Parameter, 
                data.Culture
                );

            ((IConvertBackNode<TSource1>) m_child1).ConvertBackTraversal(
                data,
                output.Item1
                );

            ((IConvertBackNode<TSource2>) m_child2).ConvertBackTraversal(
                data,
                output.Item2
                );

            ((IConvertBackNode<TSource3>) m_child3).ConvertBackTraversal(
                data,
                output.Item3
                );
        }

        #endregion
    }
}