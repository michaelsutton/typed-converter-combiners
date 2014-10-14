using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Converters.Infrastructure.Combiners.Nodes.Typed.Double;
using Converters.Infrastructure.Combiners.Nodes.Untyped;

namespace Converters.Infrastructure.Typed.Double
{
    public abstract class TypedDoubleValueConverter<TSource1, TSource2, TTarget> :
        TypedMultiConverterBase,
        ITypedDoubleValueConverter<TSource1, TSource2, TTarget>,
        ICombinableConverter
    {
        #region IMultiValueConverter Imp

        object IMultiValueConverter.Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length != 2)
            {
                return ConvertFallback(
                        values, targetType, parameter, culture,
                        new CountMismatch(values.Length, 2)
                        );
            }

            if (!(values[0] is TSource1))
            {
                return ConvertFallback(
                        values, targetType, parameter, culture,
                        new TypeMismatch(values[0], typeof(TSource1))
                        );
            }

            if (!(values[1] is TSource2))
            {
                return ConvertFallback(
                        values, targetType, parameter, culture,
                        new TypeMismatch(values[1], typeof(TSource2))
                        );
            }

            return Convert((TSource1) values[0], (TSource2) values[1], targetType, parameter, culture);
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

            var pair = ConvertBack((TTarget) value, targetTypes, parameter, culture);
            return new object[] { pair.Item1, pair.Item2 };
        }

        #endregion

        #region Typed conversion

        public abstract TTarget Convert(TSource1 value1, TSource2 value2, Type actualTargetType, object parameter, CultureInfo culture);
        public abstract IPair<TSource1, TSource2> ConvertBack(TTarget value, Type[] actualTargetTypes, object parameter, CultureInfo culture);

        #endregion

        #region ICombinableConverter Imp

        IConverterNode ICombinableConverter.GetConverterNode()
        {
            return new TypedDoubleConverterNode<TSource1, TSource2, TTarget>(this);
        }

        IConverterNode ICombinableConverter.GetConverterLeaf(IEnumerable<int> mapping)
        {
            return new TypedDoubleConverterLeaf<TSource1, TSource2, TTarget>(this, mapping);
        }

        #endregion
    }
}
