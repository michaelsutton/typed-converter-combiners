using System;
using Converters.Infrastructure.Combiners.Builders;
using Converters.Infrastructure.Combiners.Nodes.Untyped;
using Converters.Infrastructure.Utils;

namespace Converters.Infrastructure.Combiners
{
    /// <summary>
    /// A base class for converter combiners. Namely a Converter Tree and a Converter Chain.
    /// </summary>
    public class CombinerBase
    {
        #region Fields

        private IConverterNode m_root;
        private ConversionFlow m_supports;

        internal IConverterNode Root
        {
            get { return m_root; }
        }

        #endregion

        #region CTOR

        public CombinerBase()
        {
            m_supports = ConversionFlow.None;
        }

        #endregion

        #region Verification

        protected void VerifyAny()
        {
            if (!m_supports.HasFlag(ConversionFlow.Convert) && !m_supports.HasFlag(ConversionFlow.ConvertBack))
            {
                m_root = null; // clean up
                throw new InvalidConversionException("The combined converters break combiner connections on both ways. The combiner is invalid for any conversions");
            }
        }

        protected void VerifyConvert()
        {
            if (!m_supports.HasFlag(ConversionFlow.Convert))
                throw new InvalidConversionException("The converter combiner is disconnected on the Convert direction");
        }

        protected void VerifyConvertBack()
        {
            if (!m_supports.HasFlag(ConversionFlow.ConvertBack))
                throw new InvalidConversionException("The converter combiner is disconnected on the ConvertBack direction");
        }

        #endregion

        #region Build

        protected void BuildInternal(BuilderBase builder)
        {
            m_root = builder.Build(out m_supports);
        }

        #endregion
    }
}