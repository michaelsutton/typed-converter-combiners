using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Converters.Infrastructure.Combiners.Nodes.Typed;
using Converters.Infrastructure.Combiners.Traversal;

namespace Converters.Infrastructure.Combiners.Nodes.Untyped
{
    internal abstract class UntypedConverterNodeBase : IConverterNode<object>, IConverterNode
    {
        #region IConverterNode Imp

        public abstract object ConvertTraversal(ConvertTraversalData data);
        public abstract void ConvertBackTraversal(ConvertBackTraversalData data, object value);

        public virtual ConversionFlow SetChildren(IList<IConverterNode> children)
        {
            throw new NotSupportedException();
        }

        public virtual ConversionFlow SetChild(IConverterNode child)
        {
            throw new NotSupportedException();
        }

        #endregion
    }
}
