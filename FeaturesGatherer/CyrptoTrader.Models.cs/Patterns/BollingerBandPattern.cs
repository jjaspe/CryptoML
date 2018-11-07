using System;
using System.Collections.Generic;
using System.Text;

namespace CyrptoTrader.Models.Patterns
{
    public class BollingerBandPattern : Pattern
    {
        public decimal PercentPriceSpread { get; set; }

        public override string ToString()
        {
            return String.Format("BollingerBandsCloseUnderLower{0}PercentSpread", PercentPriceSpread);
        }
    }
}
