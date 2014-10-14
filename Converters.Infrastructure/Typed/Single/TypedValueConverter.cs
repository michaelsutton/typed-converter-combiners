using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using Converters.Infrastructure.Combiners.Nodes.Typed;
using Converters.Infrastructure.Combiners.Nodes.Typed.Single;
using Converters.Infrastructure.Combiners.Nodes.Untyped;
using Converters.Infrastructure.Utils;

namespace Converters.Infrastructure.Typed.Single
{
    public abstract class TypedValueConverter<TSource, TTarget> :
        TypedConverterBase, 
        ITypedValueConverter<TSource, TTarget>,
        ICombinableConverter
    {
        #region IValueConverter Imp

        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is TSource))
            {
                return ConvertFallback(
                    value, targetType, parameter, culture,
                    new TypeMismatch(value, typeof(TSource))
                        );
            }

            return Convert((TSource) value, targetType, parameter, culture);
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is TTarget))
            {
                return ConvertBackFallback(
                    value, targetType, parameter, culture,
                    new TypeMismatch(value, typeof(TTarget))
                    );
            }

            return ConvertBack((TTarget) value, targetType, parameter, culture);
        }

        #endregion

        #region Fallbacks

        protected virtual object ConvertFallback(
            object value, Type targetType, object parameter, CultureInfo culture, 
            Mismatch mismatch
            )
        {
            throw new TypedConversionException(mismatch.GetDescription());
        }

        protected virtual object ConvertBackFallback(
            object value, Type targetType, object parameter, CultureInfo culture,
            Mismatch mismatch
            )
        {
            throw new TypedConversionException(mismatch.GetDescription());
        }

        #endregion

        #region Typed conversion

        public abstract TTarget Convert(TSource value, Type actualTargetType, object parameter, CultureInfo culture);
        public abstract TSource ConvertBack(TTarget value, Type actualTargetType, object parameter, CultureInfo culture);

        #endregion

        #region ICombinableConverter Imp

        IConverterNode ICombinableConverter.GetConverterNode()
        {
            return new TypedConverterNode<TSource, TTarget>(this);
        }

        IConverterNode ICombinableConverter.GetConverterLeaf(IEnumerable<int> mapping)
        {
            return new TypedConverterLeaf<TSource, TTarget>(this, mapping.First());
        }

        #endregion
    }
}