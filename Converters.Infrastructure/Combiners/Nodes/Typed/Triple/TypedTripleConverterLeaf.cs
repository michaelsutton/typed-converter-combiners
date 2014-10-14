using System.Collections.Generic;
using Converters.Infrastructure.Combiners.Traversal;
using Converters.Infrastructure.Typed.Triple;
using Converters.Infrastructure.Utils;

namespace Converters.Infrastructure.Combiners.Nodes.Typed.Triple
{
    internal class TypedTripleConverterLeaf<TSource1, TSource2, TSource3, TTarget> : TypedConverterNodeBase<TTarget>
    {
        #region Fields

        private readonly IEnumerable<int> m_mapping;
        private readonly TypedTripleValueConverter<TSource1, TSource2, TSource3, TTarget> m_converter;

        #endregion

        #region CTOR

        public TypedTripleConverterLeaf(TypedTripleValueConverter<TSource1, TSource2, TSource3, TTarget> converter, IEnumerable<int> mapping)
        {
            m_converter = converter;
            m_mapping = mapping;
        }

        #endregion

        #region IConverterNode Imp

        public override TTarget ConvertTraversal(ConvertTraversalData data)
        {
            object[] values = data.GetValues(m_mapping);
            if (values.Length != 3 ||
                !(values[0] is TSource1) ||
                !(values[1] is TSource2) ||
                !(values[2] is TSource3))
                throw new TypedConversionException();

            return m_converter.Convert(
                (TSource1) values[0],
                (TSource2) values[1],
                (TSource3) values[2], 
                this == data.Root ? data.TargetType : typeof(TTarget),
                data.Parameter,
                data.Culture
                );
        }

        public override void ConvertBackTraversal(ConvertBackTraversalData data, TTarget value)
        {
            var output = m_converter.ConvertBack(
                value, 
                data.GetTargetTypes(m_mapping), 
                data.Parameter, 
                data.Culture
                );

            data.SetValues(
                new object[] { output.Item1, output.Item2, output.Item3 }, 
                m_mapping
                );
        }

        #endregion
    }
}