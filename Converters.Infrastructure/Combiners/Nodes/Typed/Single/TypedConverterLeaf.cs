using Converters.Infrastructure.Combiners.Traversal;
using Converters.Infrastructure.Typed.Single;
using Converters.Infrastructure.Utils;

namespace Converters.Infrastructure.Combiners.Nodes.Typed.Single
{
    internal class TypedConverterLeaf<TSource, TTarget> : TypedConverterNodeBase<TTarget>
    {
        #region Fields

        private readonly int m_mappingIndex;
        private readonly TypedValueConverter<TSource, TTarget> m_converter;

        #endregion

        #region CTOR

        public TypedConverterLeaf(TypedValueConverter<TSource, TTarget> converter, int mappingIndex)
        {
            m_converter = converter;
            m_mappingIndex = mappingIndex;
        }

        #endregion

        #region IConverterNode Imp

        public override TTarget ConvertTraversal(ConvertTraversalData data)
        {
            object value = data.GetValue(m_mappingIndex);
            if (!(value is TSource))
                throw new TypedConversionException();

            return m_converter.Convert(
                (TSource) value, 
                this == data.Root ? data.TargetType : typeof(TTarget),
                data.Parameter,
                data.Culture
                );
        }

        public override void ConvertBackTraversal(ConvertBackTraversalData data, TTarget value)
        {
            TSource output = m_converter.ConvertBack(
                value, 
                data.GetTargetType(m_mappingIndex), 
                data.Parameter, 
                data.Culture
                );

            data.SetValue(
                output, 
                m_mappingIndex
                );
        }

        #endregion
    }
}