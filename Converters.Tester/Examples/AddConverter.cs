using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Converters.Infrastructure.Typed.Double;

namespace Converters.Tester.Examples
{
    public class AddConverter : TypedDoubleValueConverter<int, int, int>
    {
        public override int Convert(int value1, int value2, Type actualTargetType, object parameter, CultureInfo culture)
        {
            return value1 + value2;
        }

        public override IPair<int, int> ConvertBack(int value, Type[] actualTargetTypes, object parameter, CultureInfo culture)
        {
            return new Pair<int, int>(0, value);
        }
    }

    public class UntypedAddConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return (int) values[0] + (int) values[1];
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return new object[] { 0, (int)value };
        }
    }
}
