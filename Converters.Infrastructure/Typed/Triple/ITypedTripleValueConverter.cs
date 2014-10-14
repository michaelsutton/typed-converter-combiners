using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Converters.Infrastructure.Typed.Triple
{
    public interface ITriple<out TSource1, out TSource2, out TSource3>
    {
        TSource1 Item1 { get; }
        TSource2 Item2 { get; }
        TSource3 Item3 { get; }
    }

    public struct Triple<TSource1, TSource2, TSource3> : ITriple<TSource1, TSource2, TSource3>
    {
        public Triple(TSource1 item1, TSource2 item2, TSource3 item3)
            : this()
        {
            Item1 = item1;
            Item2 = item2;
            Item3 = item3;
        }

        public TSource1 Item1 { get; set; }
        public TSource2 Item2 { get; set; }
        public TSource3 Item3 { get; set; }
    }

    public interface ITypedTripleConvert<in TSource1, in TSource2, in TSource3, out TTarget>
    {
        TTarget Convert(
            TSource1 value1, TSource2 value2, TSource3 value3, 
            Type actualTargetType, object parameter, CultureInfo culture
            );
    }

    public interface ITypedTripleConvertBack<out TSource1, out TSource2, out TSource3, in TTarget>
    {
        ITriple<TSource1, TSource2, TSource3> ConvertBack(
            TTarget value, Type[] actualTargetTypes, object parameter, CultureInfo culture);
    }

    public interface ITypedTripleValueConverter<TSource1, TSource2, TSource3, TTarget> :
        IMultiValueConverter,
        ITypedTripleConvert<TSource1, TSource2, TSource3, TTarget>,
        ITypedTripleConvertBack<TSource1, TSource2, TSource3, TTarget>
    {
    }
}
