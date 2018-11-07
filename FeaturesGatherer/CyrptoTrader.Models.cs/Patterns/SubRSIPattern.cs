using System;
using System.Collections.Generic;
using System.Text;

namespace CyrptoTrader.Models.Patterns
{
    public class SubRSIPattern : Pattern
    {
        public decimal Threshhold { get; set; }
        public override string ToString()
        {
            return String.Format("Sub{0}RSI", Threshhold);
        }
    }
}
