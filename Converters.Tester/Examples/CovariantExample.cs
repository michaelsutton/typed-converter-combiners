using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Converters.Infrastructure.Typed;
using Converters.Infrastructure.Typed.Single;

namespace Converters.Tester.Examples
{
    public class A1
    {
        public readonly int m_i;

        public A1(int i)
        {
            m_i = i;
        }
    }

    public class A2 : A1
    {
        public A2(int i) 
            : base(i)
        {
        }
    }

    public class A3 : A2
    {
        public A3(int i)
            : base(i)
        {
        }
    }

    public class A1ToIntConverter : TypedValueConverter<A1, int>
    {
        public override int Convert(A1 value, Type actualTargetType, object parameter, CultureInfo culture)
        {
            return value.m_i;
        }

        public override A1 ConvertBack(int value, Type actualTargetType, object parameter, CultureInfo culture)
        {
            return new A2(value);
        }
    }

    public class IntToA2Converter : TypedValueConverter<int, A2>
    {
        public override A2 Convert(int value, Type actualTargetType, object parameter, CultureInfo culture)
        {
            return new A2(value);
        }

        public override int ConvertBack(A2 value, Type actualTargetType, object parameter, CultureInfo culture)
        {
            return value.m_i;
        }
    }

    public class A3ToIntConverter : TypedValueConverter<A3, int>
    {
        public override int Convert(A3 value, Type actualTargetType, object parameter, CultureInfo culture)
        {
            return value.m_i;
        }

        public override A3 ConvertBack(int value, Type actualTargetType, object parameter, CultureInfo culture)
        {
            return new A3(value);
        }
    }
}
