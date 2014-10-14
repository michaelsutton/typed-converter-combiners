using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;
using Converters.Infrastructure.Combiners.Nodes.Typed.Many;
using Converters.Infrastructure.Combiners.Nodes.Untyped;

namespace Converters.Infrastructure.Typed.Many
{
    public abstract class TypedMultiValueConverter<TSource, TTarget> :
        TypedMultiConverterBase,
        ITypedMultiValueConverter<TSource, TTarget>,
        ICombinableConverter
    {
        #region IMultiValueConverter Imp

        object IMultiValueConverter.Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            TSource[] typed = new TSource[values.Length];
            for (int i = 0; i < values.Length; i++)
            {
                var value = values[i];
                if (!(value is TSource))
                {
                    return ConvertFallback(
                        values, targetType, parameter, culture, 
                        new TypeMismatch(value, typeof(TSource))
                        );
                }

                typed[i] = (TSource) value;
            }

            return Convert(typed, targetType, parameter, culture);
        }

        object[] IMultiValueConverter.ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            if (!(value is TTarget))
            {
                return ConvertBackFallback(
                    value, targetTypes, parameter, culture, 
                    new TypeMismatch(value, typeof(TTarget))
                    );
            }

            TSource[] typed = ConvertBack((TTarget)value, targetTypes, parameter, culture);
            object[] outputs = new object[typed.Length];

            for (int i = 0; i < outputs.Length ; i++)
                outputs[i] = typed[i]; 

            return outputs;
        }

        #endregion

        #region Typed conversion

        public abstract TTarget Convert(TSource[] value, Type actualTargetType, object parameter, CultureInfo culture);
        public abstract TSource[] ConvertBack(TTarget value, Type[] actualTargetTypes, object parameter, CultureInfo culture);

        #endregion

        #region ICombinableConverter Imp

        IConverterNode ICombinableConverter.GetConverterNode()
        {
            return new TypedMultiConverterNode<TSource, TTarget>(this);
        }

        IConverterNode ICombinableConverter.GetConverterLeaf(IEnumerable<int> mapping)
        {
            return new TypedMultiConverterLeaf<TSource, TTarget>(this, mapping);
        }

        #endregion
    }
}
