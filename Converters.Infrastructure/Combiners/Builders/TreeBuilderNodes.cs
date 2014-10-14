using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Markup;
using Converters.Infrastructure.Utils;

namespace Converters.Infrastructure.Combiners.Builders
{
    public interface ITreeBuilderNode
    {
        
    }

    [ContentProperty("Child")]
    public class ValueConverterNode : ITreeBuilderNode
    {
        public ValueConverterNode()
        {
            Mapping = -1;
        }

        public IValueConverter Converter { get; set; }
        public int Mapping { get; set; }
        public ITreeBuilderNode Child { get; set; }
    }

    public class NodeCollection : List<ITreeBuilderNode> { }

    [ContentProperty("Children")]
    public class MultiValueConverterNode : ITreeBuilderNode
    {
        public IMultiValueConverter Converter { get; set; }

        [TypeConverter(typeof(IntegersTypeConverter))]
        public IEnumerable<int> Mapping { get; set; }

        private readonly NodeCollection m_children = new NodeCollection();
        public NodeCollection Children
        {
            get { return m_children; }
        }
    }
}