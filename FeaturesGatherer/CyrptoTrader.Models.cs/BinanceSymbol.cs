using System;
using System.Collections.Generic;
using System.Text;

namespace CyrptoTrader.Models
{
    public class BinanceSymbol
    {
        public string Name { get; set; }
        public string AssetName { get; set; }
        public List<BinanceCandleStick> Periods { get; set; }
    }
}
