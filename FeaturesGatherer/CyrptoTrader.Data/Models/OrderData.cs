using Binance.Account.Orders;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace CyrptoTrader.Data
{
    public class OrderData
    {
        public OrderData(Order order)
        {
            ClientOrderId = order.ClientOrderId;
            Id = order.Id;
            Symbol = order.Symbol;
            Price = order.Price;
            OriginalQuantity = order.OriginalQuantity;
            ExecutedQuantity = order.ExecutedQuantity;
            Status = order.Status.ToString();
            TimeInForce = order.TimeInForce.ToString();
            Type = order.Type.ToString();
            Side = order.Side.ToString();
            StopPrice = order.StopPrice;
            IcebergQuantity = order.IcebergQuantity;
            Timestamp = order.Timestamp;
        }

        [BsonId]
        public string ClientOrderId { get; set; }
        public long Id { get; set; }
        public string Symbol { get; set; }
        public decimal Price { get; set; }
        public decimal OriginalQuantity { get; set; }
        public decimal ExecutedQuantity { get; set; }
        public string Status { get; set; }
        public string TimeInForce { get; set; }
        public string Type { get; set; }
        public string Side { get; set; }
        public decimal StopPrice { get; set; }
        public decimal IcebergQuantity { get; set; }
        public long Timestamp { get; set; }
    }
}
