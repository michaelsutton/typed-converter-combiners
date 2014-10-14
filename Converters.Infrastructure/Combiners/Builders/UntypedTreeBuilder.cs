using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Converters.Infrastructure.Combiners.Nodes.Untyped;

namespace Converters.Infrastructure.Combiners.Builders
{
    public class UntypedTreeBuilder : TreeBuilder
    {
        internal override IConverterNode ResolveNode(IValueConverter converter)
        {
            return new UntypedConverterNode(converter);
        }

        internal override IConverterNode ResolveLeaf(IValueConverter converter, int mappingIndex)
        {
            return new UntypedConverterLeaf(converter, mappingIndex);
        }

        internal override IConverterNode ResolveNode(IMultiValueConverter converter)
        {
            return new UntypedMultiConverterNode(converter);
        }

        internal override IConverterNode ResolveLeaf(IMultiValueConverter converter, IEnumerable<int> mapping)
        {
            return new UntypedMultiConverterLeaf(converter, mapping);
        }
    }
}
