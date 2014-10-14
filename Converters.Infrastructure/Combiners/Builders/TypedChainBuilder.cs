using System.Windows.Data;
using Converters.Infrastructure.Combiners.Nodes.Untyped;

namespace Converters.Infrastructure.Combiners.Builders
{
    public class TypedChainBuilder : ChainBuilder
    {
        internal override IConverterNode ResolveNode(IValueConverter converter)
        {
            var combinable = converter as ICombinableConverter;
            if (combinable == null)
                return new UntypedConverterNode(converter);

            return combinable.GetConverterNode();
        }

        internal override IConverterNode ResolveLeaf(IValueConverter converter)
        {
            var combinable = converter as ICombinableConverter;
            if (combinable == null)
                return new UntypedConverterLeaf(converter, 0);

            return combinable.GetConverterLeaf(new[] { 0 });
        }
    }
}