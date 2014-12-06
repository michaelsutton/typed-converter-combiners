using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Converters.Infrastructure;
using Converters.Infrastructure.Typed;
using Converters.Infrastructure.Typed.Single;
using Converters.Infrastructure.Utils;

namespace Converters.Tester.Examples
{
    public class IntToStringConverter : TypedValueConverter<int, string>
    {
        public IntToStringConverter()
        {
            
        }

        public override string Convert(int value, Type actualTargetType, object parameter, CultureInfo culture)
        {
            return value.ToString(culture);
        }

        public override int ConvertBack(string value, Type actualTargetType, object parameter, CultureInfo culture)
        {
            if (string.IsNullOrWhiteSpace(value))
                return 0;

            int num;
            if (int.TryParse(value, out num))
                return num;

            throw new InvalidConversionException();
        }
    }

    public class UntypedIntToStringConverter : ThisExtension, IValueConverter
    {
        public UntypedIntToStringConverter()
        {
            
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int num;
            if (int.TryParse(value as string, out num))
                return num;
            throw new InvalidDataException();
        }
    }
}
