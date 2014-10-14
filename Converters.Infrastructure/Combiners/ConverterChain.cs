using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;
using Converters.Infrastructure.Combiners.Builders;
using Converters.Infrastructure.Combiners.Nodes.Untyped;
using Converters.Infrastructure.Combiners.Traversal;

namespace Converters.Infrastructure.Combiners
{
    [ContentProperty("Builder")]
    public class ConverterChain : CombinerBase, IValueConverter
    {
        #region IValueConverter Imp

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            VerifyConvert();

            var root = Root;
            ConvertChainTraversalData data = new ConvertChainTraversalData(
                value,
                targetType,
                parameter,
                culture,
                root
                );

            return root.ConvertTraversal(data);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            VerifyConvertBack();

            ConvertBackChainTraversalData data = new ConvertBackChainTraversalData(
                targetType,
                parameter,
                culture
                );

            Root.ConvertBackTraversal(data, value);
            return data.GetOutput();
        }

        #endregion

        #region Chain Builder

        public ChainBuilder Builder
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
