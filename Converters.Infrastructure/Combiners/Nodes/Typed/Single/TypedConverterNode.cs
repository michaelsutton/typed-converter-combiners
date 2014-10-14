using Converters.Infrastructure.Combiners.Nodes.Untyped;
using Converters.Infrastructure.Combiners.Traversal;
using Converters.Infrastructure.Typed.Single;

namespace Converters.Infrastructure.Combiners.Nodes.Typed.Single
{
    internal class TypedConverterNode<TSource, TTarget> : TypedConverterNodeBase<TTarget>
    {
        #region Fields

        private IConverterNode m_child;
        private readonly TypedValueConverter<TSource, TTarget> m_converter;

        #endregion

        #region CTOR

        public TypedConverterNode(TypedValueConverter<TSource, TTarget> converter)
        {
            m_converter = converter;
        }

        #endregion

        #region IConverterNode Imp

        public override ConversionFlow SetChild(IConverterNode child)
        {
            m_child = child;
            return GetSupportedConversionFlows<TSource>(child);
        }

        public override TTarget ConvertTraversal(ConvertTraversalData data)
        {
            TSource value = ((IConvertNode<TSource>) m_child).ConvertTraversal(data);

            return m_converter.Convert(
                value, 
                this == data.Root ? data.TargetType : typeof(TTarget),
                data.Parameter,
                data.Culture
                );
        }

        public override void ConvertBackTraversal(ConvertBackTraversalData data, TTarget value)
        {
            TSource output = m_converter.ConvertBack(
                value, 
                typeof (TSource), 
                data.Parameter, 
                data.Culture
                );

            ((IConvertBackNode<TSource>) m_child).ConvertBackTraversal(
                data,
                output
                );
        }

        #endregion
    }
}