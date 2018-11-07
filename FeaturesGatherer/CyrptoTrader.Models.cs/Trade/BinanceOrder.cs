using Binance.Account.Orders;
using CyrptoTrader.Models.Patterns;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CyrptoTrader.Models
{
    public class BinanceOrder
    {
        public BinanceOrder()
        {
            BuyOrder = new NewOrder();
            SellOrder = new NewOrder();
            Id = Guid.NewGuid().ToString();
            CreatedDate = DateTime.UtcNow;
        }
        public BinanceOrder(string id)
        {
            Id = id;
        }
        public string Id { get; set; }
        public NewOrder BuyOrder { get; set; }
        public NewOrder SellOrder { get; set; }
        public Pattern Pattern { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        public decimal BitcoinPriceAtPurchase { get; set; }
        public decimal NewTargetSellPrice { get; set; }
        public bool startSellTracking { get; set; }
        public BinanceCandleStick periodUsedForStairStep { get; set; }
        public decimal BitcoinPriceAtSell { get; set; }
        public decimal BitcoinProfits { get; set; }
        public bool SoldOnTime { get; set; }
    }
}
