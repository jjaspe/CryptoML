using CyrptoTrader.Models.Patterns;
using System;
using System.Collections.Generic;
using System.Text;

namespace CyrptoTrader.Models
{
    public class InBetweenPattern : Pattern
    {
        public int Candles { get; set; }
        public override string ToString()
        {
            return String.Format("{0}CandlesInBetween", Candles);
        }
    }
}
