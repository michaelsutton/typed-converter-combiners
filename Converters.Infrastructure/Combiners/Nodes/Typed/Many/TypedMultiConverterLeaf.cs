using System.Collections.Generic;
using Converters.Infrastructure.Combiners.Traversal;
using Converters.Infrastructure.Typed.Many;
using Converters.Infrastructure.Utils;

namespace Converters.Infrastructure.Combiners.Nodes.Typed.Many
{
    internal class TypedMultiConverterLeaf<TSource, TTarget> : TypedConverterNodeBase<TTarget>
    {
        #region Fields

        private readonly IEnumerable<int> m_mapping;
        private readonly TypedMultiValueConverter<TSource, TTarget> m_converter;

        #endregion

        #region CTOR

        public TypedMultiConverterLeaf(TypedMultiValueConverter<TSource, TTarget> converter, IEnumerable<int> mapping)
        {
            m_converter = converter;
            m_mapping = mapping;
        }

        #endregion

        #region IConverterNode Imp

        public override TTarget ConvertTraversal(ConvertTraversalData data)
        {
            var values = data.GetValues(m_mapping);
            TSource[] typed = new TSource[values.Length];
            for (int i = 0; i < values.Length; i++)
            {
                var value = values[i];
                if (!(value is TSource))
                {
                    throw new TypedConversionException();
                }

                typed[i] = (TSource)value;
            }

            return m_converter.Convert(
                typed,
                this == data.Root ? data.TargetType : typeof(object),
                data.Parameter,
                data.Culture
                );
        }

        public override void ConvertBackTraversal(ConvertBackTraversalData data, TTarget value)
        {
            TSource[] typed = m_converter.ConvertBack(
                value,
                data.GetTargetTypes(m_mapping),
                data.Parameter,
                data.Culture
                );

            object[] outputs = new object[typed.Length];

            for (int i = 0; i < outputs.Length; i++)
                outputs[i] = typed[i]; 

            data.SetValues(outputs, m_mapping);
        }

        #endregion
    }
}