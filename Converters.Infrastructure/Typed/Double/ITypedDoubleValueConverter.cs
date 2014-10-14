using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Converters.Infrastructure.Typed.Double
{
    public interface IPair<out TSource1, out TSource2>
    {
        TSource1 Item1 { get; }
        TSource2 Item2 { get; }
    }

    public struct Pair<TSource1, TSource2> : IPair<TSource1, TSource2>
    {
        public Pair(TSource1 item1, TSource2 item2) : this()
        {
            Item1 = item1;
            Item2 = item2;
        }

        public TSource1 Item1 { get; set; }
        public TSource2 Item2 { get; set; }
    }

    public interface ITypedDoubleConvert<in TSource1, in TSource2, out TTarget>
    {
        TTarget Convert(TSource1 value1, TSource2 value2, Type actualTargetType, object parameter, CultureInfo culture);
    }

    public interface ITypedDoubleConvertBack<out TSource1, out TSource2, in TTarget>
    {
        IPair<TSource1, TSource2> ConvertBack(TTarget value, Type[] actualTargetTypes, object parameter, CultureInfo culture);
    }

    public interface ITypedDoubleValueConverter<TSource1, TSource2, TTarget> :
        IMultiValueConverter,
        ITypedDoubleConvert<TSource1, TSource2, TTarget>,
        ITypedDoubleConvertBack<TSource1, TSource2, TTarget> 
    {
    }
}
