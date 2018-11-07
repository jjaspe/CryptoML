using CyrptoTrader.Models;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace CyrptoTrader.Data
{
    public class BinanceOrderData
    {
        public BinanceOrderData(BinanceOrder order)
        {
            Id = order.Id;
            BuyOrder = new NewOrderData(order.BuyOrder);
            SellOrder = new NewOrderData(order.SellOrder);
            Pattern = new PatternData(order.Pattern);
            CreatedDate = order.CreatedDate;
            CompletedDate = order.CompletedDate;
            BitcoinPriceAtPurchase = order.BitcoinPriceAtPurchase;
            BitcoinPriceAtSell = order.BitcoinPriceAtSell;
            BitcoinProfits = order.BitcoinProfits;
            SoldOnTime = order.SoldOnTime;
        }
        [BsonId]
        public string Id { get; set; }
        public NewOrderData BuyOrder { get; set; }
        public NewOrderData SellOrder { get; set; }
        public PatternData Pattern { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        public decimal BitcoinPriceAtPurchase { get; set; }
        public decimal BitcoinPriceAtSell { get; set; }
        public decimal BitcoinProfits { get; set; }
        public bool SoldOnTime { get; set; }
    }
}
