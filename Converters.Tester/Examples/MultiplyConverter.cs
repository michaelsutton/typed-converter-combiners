using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;
using Converters.Infrastructure;
using Converters.Infrastructure.Typed;
using Converters.Infrastructure.Typed.Single;

namespace Converters.Tester.Examples
{
    public class MultiplyConverter : TypedValueConverter<int, int>
    {
        private int m_factor;
        public int Factor
        {
            get { return m_factor; }
            set { m_factor = value; }
        }

        public MultiplyConverter()
            : this(1)
        {
            
        }

        public MultiplyConverter(int factor)
        {
            m_factor = factor;
        }

        public override int Convert(int value, Type actualTargetType, object parameter, CultureInfo culture)
        {
            return value * m_factor;
        }

        public override int ConvertBack(int value, Type actualTargetType, object parameter, CultureInfo culture)
        {
            return value / m_factor;
        }
    }

    public class UntypedMultiplyConverter : IValueConverter
    {
        private int m_factor;
        public int Factor
        {
            get { return m_factor; }
            set { m_factor = value; }
        }

        public UntypedMultiplyConverter()
            : this(1)
        {

        }

        public UntypedMultiplyConverter(int factor)
        {
            m_factor = factor;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (int)value * m_factor;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (int)value / m_factor;
        }
    }
}
