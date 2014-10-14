using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows.Documents;

namespace Converters.Infrastructure.Utils
{
    public class IntegersTypeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            var str = value as string;
            if(str == null)
                throw new ArgumentException("value must be a string");

            int i = str.IndexOf('-');
            if ( i >= 0 )
            {
                if(i != str.LastIndexOf('-'))
                    throw new FormatException("string can contain only 0 to 1 '-' chars");

                int left;
                if(!int.TryParse(str.Substring(0, i), out left))
                    throw new FormatException();

                int right;
                if (!int.TryParse(str.Substring(i + 1), out right))
                    throw new FormatException();

                if(left < 0 || right < 0 || left > right)
                    throw new FormatException("invalid index range");

                return Enumerable.Range(left, right - left + 1);
            }

            var values = str.Split(new[] { ',' });
            List<int> integers = new List<int>(values.Length);

            foreach (var s in values)
            {
                int integer;
                if (!int.TryParse(s, out integer))
                    throw new FormatException();

                if (integer < 0)
                    throw new FormatException("invalid index");

                integers.Add(integer);
            }

            if(integers.Count == 0)
                throw new FormatException();

            return integers.ToArray();
        }
    }
}
