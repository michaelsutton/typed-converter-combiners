using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;
using Converters.Infrastructure.Combiners.Builders;
using Converters.Infrastructure.Combiners.Traversal;

namespace Converters.Infrastructure.Combiners
{
    [ContentProperty("Builder")]
    public class ConverterTree : CombinerBase, IMultiValueConverter
    {
        #region IMultiValueConverter Imp

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            VerifyConvert();

            var root = Root;
            ConvertTreeTraversalData data = new ConvertTreeTraversalData(
                values,
                targetType, 
                parameter,
                culture,
                root
                );

            return root.ConvertTraversal(data);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            VerifyConvertBack();

            ConvertBackTreeTraversalData data = new ConvertBackTreeTraversalData(
                targetTypes,
                parameter,
                culture
                );

            Root.ConvertBackTraversal(data, value);
            return data.GetOutputs();
        }

        #endregion

        #region Tree Builder

        public TreeBuilder Builder
        {
            get { return null; }
            set
            {
                BuildInternal(value);
                VerifyAny();
            }
        }

        #endregion
    }
}
