using CyrptoTrader.Models;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace CyrptoTrader.Data
{
    public class NewOrderData
    {
        public NewOrderData(NewOrder order)
        {
            Id = order.Id;
            Symbol = order.Symbol;
            Side = order.Side.ToString();
            Type = order.Type.ToString();
            OrderStatus = order.OrderStatus.ToString();
            TimeInForce = order.TimeInForce;
            Quantity = order.Quantity;
            Price = order.Price;
            NewClientOrderId = order.NewClientOrderId;
            StopPrice = order.StopPrice;
            IcebergQty = order.IcebergQty;
            RecvWindow = order.RecvWindow;
            order.Timestamp = order.Timestamp;
        }

        [BsonId]
        public string Id { get; set; }
        public string Symbol { get; set; }
        public string Side { get; set; }
        public string Type { get; set; }
        public string OrderStatus { get; set; }
        public string TimeInForce { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public string NewClientOrderId { get; set; }
        public decimal StopPrice { get; set; }
        public decimal IcebergQty { get; set; }
        public long RecvWindow { get; set; }
        public long Timestamp { get; set; }
    }
}
