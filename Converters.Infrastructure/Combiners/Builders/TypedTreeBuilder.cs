using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Converters.Infrastructure.Combiners.Nodes.Untyped;

namespace Converters.Infrastructure.Combiners.Builders
{
    public class TypedTreeBuilder : TreeBuilder
    {
        internal override IConverterNode ResolveNode(IValueConverter converter)
        {
            var combinable = converter as ICombinableConverter;
            if (combinable == null)
                return new UntypedConverterNode(converter);

            return combinable.GetConverterNode();
        }

        internal override IConverterNode ResolveLeaf(IValueConverter converter, int mapping)
        {
            var combinable = converter as ICombinableConverter;
            if (combinable == null)
                return new UntypedConverterLeaf(converter, mapping);

            return combinable.GetConverterLeaf(new[] { mapping });
        }

        internal override IConverterNode ResolveNode(IMultiValueConverter converter)
        {
            var combinable = converter as ICombinableConverter;
            if (combinable == null)
                return new UntypedMultiConverterNode(converter);

            return combinable.GetConverterNode();
        }

        internal override IConverterNode ResolveLeaf(IMultiValueConverter converter, IEnumerable<int> mapping)
        {
            var combinable = converter as ICombinableConverter;
            if (combinable == null)
                return new UntypedMultiConverterLeaf(converter, mapping);

            return combinable.GetConverterLeaf(mapping);
        }
    }
}
