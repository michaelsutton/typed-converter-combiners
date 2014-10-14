using System;
using System.Globalization;
using System.Windows.Data;

namespace Converters.Infrastructure.Typed.Single
{
    public interface ITypedConvert<in TSource, out TTarget>
    {
        TTarget Convert(TSource value, Type actualTargetType, object parameter, CultureInfo culture);
    }

    public interface ITypedConvertBack<out TSource, in TTarget>
    {
        TSource ConvertBack(TTarget value, Type actualTargetType, object parameter, CultureInfo culture);
    }

    public interface ITypedValueConverter<TSource, TTarget> : 
        IValueConverter,
        ITypedConvert<TSource, TTarget>, 
        ITypedConvertBack<TSource, TTarget>
    {
    }
}
