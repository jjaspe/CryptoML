using CyrptoTrader.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CyrptoTrader.Data
{
    public class BinanceSymbolData
    {
        public BinanceSymbolData(BinanceSymbol symbol)
        {
            Id = Guid.NewGuid().ToString();
            Name = symbol.Name;
            AssetName = symbol.AssetName;
            ThirtyMinPeriods = new List<BinanceCandleStickData>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string AssetName { get; set; }
        public List<BinanceCandleStickData> ThirtyMinPeriods { get; set; }
    }
}
