using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Converters.Infrastructure.Combiners.Nodes.Typed.Triple;
using Converters.Infrastructure.Combiners.Nodes.Untyped;

namespace Converters.Infrastructure.Typed.Triple
{
    public abstract class TypedTripleValueConverter<TSource1, TSource2, TSource3, TTarget> :
        TypedMultiConverterBase,
        ITypedTripleValueConverter<TSource1, TSource2, TSource3, TTarget>,
        ICombinableConverter
    {
        #region IMultiValueConverter Imp

        object IMultiValueConverter.Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length != 3)
            {
                return ConvertFallback(
                        values, targetType, parameter, culture,
                        new CountMismatch(values.Length, 3)
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

            if (!(values[2] is TSource3))
            {
                return ConvertFallback(
                        values, targetType, parameter, culture,
                        new TypeMismatch(values[2], typeof(TSource3))
                        );
            }

            return Convert((TSource1)values[0], (TSource2)values[1], (TSource3)values[2], targetType, parameter, culture);
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

            var triple = ConvertBack((TTarget)value, targetTypes, parameter, culture);
            return new object[] { triple.Item1, triple.Item2, triple.Item3 };
        }

        #endregion

        #region Typed conversion

        public abstract TTarget Convert(
            TSource1 value1, TSource2 value2, TSource3 value3, 
            Type actualTargetType, object parameter, CultureInfo culture
            );

        public abstract ITriple<TSource1, TSource2, TSource3> ConvertBack(
            TTarget value, Type[] actualTargetTypes, object parameter, CultureInfo culture
            );

        #endregion

        #region ICombinableConverter Imp

        IConverterNode ICombinableConverter.GetConverterNode()
        {
            return new TypedTripleConverterNode<TSource1, TSource2, TSource3, TTarget>(this);
        }

        IConverterNode ICombinableConverter.GetConverterLeaf(IEnumerable<int> mapping)
        {
            return new TypedTripleConverterLeaf<TSource1, TSource2, TSource3, TTarget>(this, mapping);
        }

        #endregion
    }
}
