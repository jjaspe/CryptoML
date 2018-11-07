using Binance.Account.Orders;
using System;
using System.ComponentModel.DataAnnotations;

namespace CyrptoTrader.Models
{
    public class NewOrder
    {
        public NewOrder()
        {
            Id = Guid.NewGuid().ToString();
        }

        public NewOrder(string id)
        {
            Id = id;
        }

        public string Id { get; set; }
        public string Symbol { get; set; }
        public OrderSide Side { get; set; }
        public OrderType Type { get; set; }
        public OrderStatus OrderStatus { get; set; }
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
