using System;
using System.Collections.Generic;
using System.Text;

namespace CyrptoTrader.Models.Patterns
{
    public class DropOffPattern : Pattern
    {
        public decimal DropOffPercent { get; set; }
        public int Candles { get; set; }
        public override string ToString()
        {
            return String.Format("{0}Candles{1}PercentDropOff", Candles, DropOffPercent);
        }
    }
}
