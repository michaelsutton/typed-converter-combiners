using System;
using System.Collections.Generic;
using Converters.Infrastructure.Combiners.Traversal;

namespace Converters.Infrastructure.Combiners.Nodes.Untyped
{
    [Flags]
    internal enum ConversionFlow
    {
        None = 0,
        Convert = 1,
        ConvertBack = 2,
        Bidirectional = Convert | ConvertBack
    }

    internal interface IConvertNode
    {
        object ConvertTraversal(ConvertTraversalData data);
    }

    internal interface IConvertBackNode
    {
        void ConvertBackTraversal(ConvertBackTraversalData data, object value);
    }

    internal interface IConverterNode :
        IConvertNode,
        IConvertBackNode
    {
        ConversionFlow SetChildren(IList<IConverterNode> children);
        ConversionFlow SetChild(IConverterNode child);
    }

    internal interface ICombinableConverter
    {
        IConverterNode GetConverterNode();
        IConverterNode GetConverterLeaf(IEnumerable<int> mapping);
    }
}
