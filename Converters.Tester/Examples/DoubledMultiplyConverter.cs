using System.Windows.Controls;
using Converters.Infrastructure;
using Converters.Infrastructure.Combiners;
using Converters.Infrastructure.Combiners.Builders;

namespace Converters.Tester.Examples
{
    public class DoubledMultiplyConverter : ConverterChain
    {
        public DoubledMultiplyConverter(int factor1, int factor2)
        {
            TypedChainBuilder builder = new TypedChainBuilder();
            builder.Converters.Add(new IntToStringConverter());

            var multiplier = new MultiplyFactorConverter(factor1);
            for (int i = 0; i < factor2; i++)
            {
                builder.Converters.Add(multiplier);
            }

            Builder = builder;
        }
    }

    public class UntypedDoubledMultiplyConverter : ConverterChain
    {
        public UntypedDoubledMultiplyConverter(int factor1, int factor2)
        {
            UntypedChainBuilder builder = new UntypedChainBuilder();
            builder.Converters.Add(new UntypedIntToStringConverter());

            var multiplier = new UntypedMultiplyConverter(factor1);
            for (int i = 0; i < factor2; i++)
            {
                builder.Converters.Add(multiplier);
            }

            Builder = builder;
        }
    }
}
