using System.Collections.Generic;
using Converters.Infrastructure.Combiners.Traversal;
using Converters.Infrastructure.Typed.Double;
using Converters.Infrastructure.Typed.Single;
using Converters.Infrastructure.Utils;

namespace Converters.Infrastructure.Combiners.Nodes.Typed.Double
{
    internal class TypedDoubleConverterLeaf<TSource1, TSource2, TTarget> : TypedConverterNodeBase<TTarget>
    {
        #region Fields

        private readonly IEnumerable<int> m_mapping;
        private readonly TypedDoubleValueConverter<TSource1, TSource2, TTarget> m_converter;

        #endregion

        #region CTOR

        public TypedDoubleConverterLeaf(TypedDoubleValueConverter<TSource1, TSource2, TTarget> converter, IEnumerable<int> mapping)
        {
            m_converter = converter;
            m_mapping = mapping;
        }

        #endregion

        #region IConverterNode Imp

        public override TTarget ConvertTraversal(ConvertTraversalData data)
        {
            object[] values = data.GetValues(m_mapping);
            if (values.Length != 2 ||
                !(values[0] is TSource1) ||
                !(values[1] is TSource2))
                throw new TypedConversionException();

            return m_converter.Convert(
                (TSource1)values[0],
                (TSource2) values[1], 
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
                new object[] { output.Item1, output.Item2 }, 
                m_mapping
                );
        }

        #endregion
    }
}