using Converters.Infrastructure.Combiners.Nodes.Untyped;
using Converters.Infrastructure.Combiners.Traversal;

namespace Converters.Infrastructure.Combiners.Nodes.Typed
{
    internal interface IConvertNode<out TTarget> : IConvertNode
    {
        new TTarget ConvertTraversal(ConvertTraversalData data);
    }

    internal interface IConvertBackNode<in TTarget> : IConvertBackNode
    {
        void ConvertBackTraversal(ConvertBackTraversalData data, TTarget value);
    }

    internal interface IConverterNode<TTarget> :
        IConverterNode,
        IConvertNode<TTarget>,
        IConvertBackNode<TTarget>
    {
    }
}
