using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Markup;
using Converters.Infrastructure.Combiners.Nodes.Untyped;
using Converters.Infrastructure.Utils;

namespace Converters.Infrastructure.Combiners.Builders
{
    [ContentProperty("Root")]
    public abstract class TreeBuilder : BuilderBase
    {
        public ITreeBuilderNode Root { get; set; }

        internal abstract IConverterNode ResolveNode(IValueConverter converter);
        internal abstract IConverterNode ResolveLeaf(IValueConverter converter, int mappingIndex);

        internal abstract IConverterNode ResolveNode(IMultiValueConverter converter);
        internal abstract IConverterNode ResolveLeaf(IMultiValueConverter converter, IEnumerable<int> mapping);

        internal override IConverterNode Build(out ConversionFlow supports)
        {
            var root = Root;
            if (root == null)
            {
                supports = ConversionFlow.None;
                return null;
            }

            supports = ConversionFlow.Bidirectional;
            return BuildTree(root, ref supports);
        }

        private IConverterNode BuildTree(ITreeBuilderNode builderNode, ref ConversionFlow supports)
        {
            var single = builderNode as ValueConverterNode;
            if (single != null)
            {
                if (single.Converter == null)
                    throw new ArgumentException("The converter instance is null");
                return BuildTree(single, ref supports);
            }

            var multi = builderNode as MultiValueConverterNode;
            if (multi != null)
            {
                if (multi.Converter == null)
                    throw new ArgumentException("The converter instance is null");
                return BuildTree(multi, ref supports);
            }

            throw new InvalidOperationException(
                string.Format(
                    "Node must be an instance of the {0} class, or the {1} class",
                    typeof(ValueConverterNode),
                    typeof(MultiValueConverterNode)
                )
            );
        }

        private IConverterNode BuildTree(ValueConverterNode builderNode, ref ConversionFlow supports)
        {
            if (builderNode.Child == null)
            {
                if(builderNode.Mapping < 0)
                    throw new ArgumentException("The node has no child so it must define a valid mapping index");
                return ResolveLeaf(builderNode.Converter, builderNode.Mapping);
            }
            else
            {
                IConverterNode current = ResolveNode(builderNode.Converter);
                var child = BuildTree(builderNode.Child, ref supports);

                supports &= current.SetChild(child);
                return current;
            }
        }

        private IConverterNode BuildTree(MultiValueConverterNode builderNode, ref ConversionFlow supports)
        {
            if (builderNode.Children.Count == 0)
            {
                if (builderNode.Mapping == null)
                    throw new ArgumentException("The node has no children so it must define a valid mapping index");
                return ResolveLeaf(builderNode.Converter, builderNode.Mapping);
            }
            else
            {
                IConverterNode current = ResolveNode(builderNode.Converter);
                List<IConverterNode> nodes = new List<IConverterNode>(builderNode.Children.Count);

                foreach (var child in builderNode.Children)
                {
                    nodes.Add(BuildTree(child, ref supports));
                }

                supports &= current.SetChildren(nodes);
                return current;
            }
        }
    }
}
