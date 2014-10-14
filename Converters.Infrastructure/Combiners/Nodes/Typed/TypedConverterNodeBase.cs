using System;
using System.Collections.Generic;
using Converters.Infrastructure.Combiners.Nodes.Untyped;
using Converters.Infrastructure.Combiners.Traversal;
using Converters.Infrastructure.Utils;

namespace Converters.Infrastructure.Combiners.Nodes.Typed
{
    internal abstract class TypedConverterNodeBase<TTarget> : IConverterNode<TTarget>
    {
        #region Typed IConverterNode 

        public abstract TTarget ConvertTraversal(ConvertTraversalData data);
        public abstract void ConvertBackTraversal(ConvertBackTraversalData data, TTarget value);

        #endregion

        #region Untyped IConverterNode 

        object IConvertNode.ConvertTraversal(ConvertTraversalData data)
        {
            // Forwarding call to typed Imp
            return ConvertTraversal(data);
        }

        void IConvertBackNode.ConvertBackTraversal(ConvertBackTraversalData data, object value)
        {
            if (!(value is TTarget)) 
                throw new TypedConversionException();

            // Forwarding call to typed Imp
            ConvertBackTraversal(data, (TTarget) value);
        }

        public virtual ConversionFlow SetChildren(IList<IConverterNode> children)
        {
            throw new NotSupportedException();
        }

        public virtual ConversionFlow SetChild(IConverterNode child)
        {
            throw new NotSupportedException();
        }

        #endregion

        #region Helpers

        protected static ConversionFlow GetSupportedConversionFlows<TSource>(IConverterNode child)
        {
            ConversionFlow supports = ConversionFlow.None;
            if (child is IConvertNode<TSource>)
                supports |= ConversionFlow.Convert;

            if (child is IConvertBackNode<TSource>)
                supports |= ConversionFlow.ConvertBack;
            return supports;
        }

        #endregion
    }
}