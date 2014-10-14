using System.Windows.Data;
using System.Windows.Markup;
using Converters.Infrastructure.Combiners.Nodes.Untyped;
using Converters.Infrastructure.Utils;

namespace Converters.Infrastructure.Combiners.Builders
{
    [ContentProperty("Converters")]
    public abstract class ChainBuilder : BuilderBase
    {
        private readonly ConverterCollection m_converters = new ConverterCollection();

        internal abstract IConverterNode ResolveNode(IValueConverter converter);
        internal abstract IConverterNode ResolveLeaf(IValueConverter converter);

        internal override IConverterNode Build(out ConversionFlow supports)
        {
            if (m_converters.Count == 0)
            {
                supports = ConversionFlow.None;
                return null;
            }

            supports = ConversionFlow.Bidirectional;

            if (m_converters.Count == 1)
                return ResolveLeaf(m_converters[0]);

            IConverterNode head = ResolveNode(m_converters[0]);
            IConverterNode current = head;

            for (int i = 1; i < m_converters.Count - 1; i++)
            {
                IConverterNode node = ResolveNode(m_converters[i]);
                supports &= current.SetChild(node);
                current = node;
            }

            IConverterNode tail = ResolveLeaf(m_converters[m_converters.Count - 1]);
            supports &= current.SetChild(tail);

            return head;
        }

        public ConverterCollection Converters
        {
            get { return m_converters; }
        }
    }
}