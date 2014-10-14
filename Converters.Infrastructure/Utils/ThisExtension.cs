using System;
using System.Windows.Markup;

namespace Converters.Infrastructure.Utils
{
    public abstract class ThisExtension : MarkupExtension
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
