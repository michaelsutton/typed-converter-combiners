using System.Windows.Data;
using Converters.Infrastructure.Combiners.Nodes.Untyped;

namespace Converters.Infrastructure.Combiners.Builders
{
    public class UntypedChainBuilder : ChainBuilder
    {
        internal override IConverterNode ResolveNode(IValueConverter converter)
        {
            return new UntypedConverterNode(converter);
        }

        internal override IConverterNode ResolveLeaf(IValueConverter converter)
        {
            return new UntypedConverterLeaf(converter, 0);
        }
    }
}
