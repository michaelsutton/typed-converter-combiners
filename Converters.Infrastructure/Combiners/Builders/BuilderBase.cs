using Converters.Infrastructure.Combiners.Nodes.Untyped;

namespace Converters.Infrastructure.Combiners.Builders
{
    public abstract class BuilderBase
    {
        internal abstract IConverterNode Build(out ConversionFlow supports);
    }
}