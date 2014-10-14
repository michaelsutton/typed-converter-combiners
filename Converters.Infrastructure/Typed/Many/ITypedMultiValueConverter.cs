using System;
using System.Globalization;
using System.Windows.Data;

namespace Converters.Infrastructure.Typed.Many
{
    public interface ITypedMultiConvert<in TSource, out TTarget>
    {
        TTarget Convert(TSource[] value, Type actualTargetType, object parameter, CultureInfo culture);
    }

    public interface ITypedMultiConvertBack<out TSource, in TTarget>
    {
        TSource[] ConvertBack(TTarget value, Type[] actualTargetTypes, object parameter, CultureInfo culture);
    }

    public interface ITypedMultiValueConverter<TSource, TTarget> :
        IMultiValueConverter,
        ITypedMultiConvert<TSource, TTarget>,
        ITypedMultiConvertBack<TSource, TTarget>
    {
    }
}
