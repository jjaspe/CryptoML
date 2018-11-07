using Binance;
using System;
using System.Collections.Generic;
using System.Text;

namespace CyrptoTrader.Models
{
    public class PatternGrouping
    {
        public decimal MinimumPrice { get; set; }
        public decimal MaximumPrice { get; set; }
        public List<Symbol> Symbols { get; set; } = new List<Symbol>();
        public List<decimal> price { get; set; } = new List<decimal>();

    }
}
