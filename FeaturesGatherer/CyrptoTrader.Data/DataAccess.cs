using CyrptoTrader.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CyrptoTrader.Data
{
    public class DataAccess : IDataAccess
    {
        private static IMongoClient client;
        private static IMongoDatabase db;
        private static IMongoCollection<BinanceCandleStickData> candleStickCollection { get; set; }
        private static IMongoCollection<BinanceOrderData> inProgressBinanceOrderCollection { get; set; }
        private static IMongoCollection<BinanceOrderData> completedBinanceOrderCollection { get; set; }
        private static IMongoCollection<OrderData> placedOrderCollection { get; set; }
        private static IMongoCollection<OrderData> marketOrderCollection { get; set; }
        private static IMongoCollection<OrderData> canceledOrderCollection { get; set; }
        private static IMongoCollection<OrderData> sellOrderCollection { get; set; }
        private static IMongoCollection<OrderData> soldOrderCollection { get; set; }
        private static IMongoCollection<BinanceSymbolData> symbolDataCollection { get; set; }

        public DataAccess()
        {
            client = new MongoClient();
            db = client.GetDatabase("CyrptoTrader");
            inProgressBinanceOrderCollection = db.GetCollection<BinanceOrderData>("InProgressBinanceOrders");
            completedBinanceOrderCollection = db.GetCollection<BinanceOrderData>("CompletedBinanceOrders");
            candleStickCollection = db.GetCollection<BinanceCandleStickData>("BinanceCandleStick");
            placedOrderCollection = db.GetCollection<OrderData>("PlacedOrders");
            marketOrderCollection = db.GetCollection<OrderData>("MarketSellOrders");
            canceledOrderCollection = db.GetCollection<OrderData>("CancledOrders");
            sellOrderCollection = db.GetCollection<OrderData>("SellOrders");
            soldOrderCollection = db.GetCollection<OrderData>("SoldOrders");
            symbolDataCollection = db.GetCollection<BinanceSymbolData>("SymbolData");
        }

        public async Task<List<BinanceSymbolData>> GetSymbolData()
        {
            return await symbolDataCollection.Find(_ => true).ToListAsync();
        }

        public async Task InsertBulkAsyncSymbolData(List<BinanceSymbolData> list)
        {
            await symbolDataCollection.InsertManyAsync(list);
        }

        public async Task DeleteBulkAsyncSymbolData()
        {
            await symbolDataCollection.DeleteManyAsync(_ => true);
        }

        #region BinanceOrder Methods
        public async Task InsertAsyncInProgressBinanceOrder(BinanceOrderData binanceOrder)
        {
            await inProgressBinanceOrderCollection.InsertOneAsync(binanceOrder);
        }

        public async Task InsertAsyncCompletedBinanceOrders(BinanceOrderData binanceOrder)
        {
            await completedBinanceOrderCollection.InsertOneAsync(binanceOrder);
        }

        public async Task<List<BinanceOrderData>> GetBulkAsyncBinanceOrders(BinanceOrderStatus status)
        {
            List<BinanceOrderData> orderData = new List<BinanceOrderData>();
            switch (status)
            {
                case BinanceOrderStatus.Completed:
                    orderData = await completedBinanceOrderCollection.Find(_ => true).ToListAsync();
                    break;
                case BinanceOrderStatus.InProgress:
                    orderData = await inProgressBinanceOrderCollection.Find(_ => true).ToListAsync();
                    break;
            }

            return orderData;
        }

        public async Task RemoveAsyncInProgressBinanceOrder(BinanceOrderData binanceOrder)
        {
            var builder = Builders<BinanceOrderData>.Filter;
            var filter = builder.Eq("Id", binanceOrder.Id);
            await inProgressBinanceOrderCollection.DeleteOneAsync(filter);
        }

        public Task RemoveAsyncCompletedBinanceOrder(BinanceOrderData binanceOrder)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Order Methods
        public async Task<List<OrderData>> GetBulkAsyncOrder(ApiOrderStatus status)
        {
            List<OrderData> orderDataList = new List<OrderData>();

            switch (status)
            {
                case ApiOrderStatus.Placed:
                    orderDataList.AddRange(await placedOrderCollection.Find(_ => true).ToListAsync());
                    break;
                case ApiOrderStatus.MarketSold:
                    orderDataList.AddRange(await marketOrderCollection.Find(_ => true).ToListAsync());
                    break;
                case ApiOrderStatus.Sell:
                    orderDataList.AddRange(await sellOrderCollection.Find(_ => true).ToListAsync());
                    break;
                case ApiOrderStatus.Sold:
                    orderDataList.AddRange(await soldOrderCollection.Find(_ => true).ToListAsync());
                    break;
                case ApiOrderStatus.Canceled:
                    orderDataList.AddRange(await canceledOrderCollection.Find(_ => true).ToListAsync());
                    break;
            }

            return orderDataList;
        }

        public async Task InsertBulkAsyncOrders(List<OrderData> list, ApiOrderStatus status)
        {
            switch (status)
            {
                case ApiOrderStatus.Placed:
                    await placedOrderCollection.InsertManyAsync(list);
                    break;
                case ApiOrderStatus.MarketSold:
                    await marketOrderCollection.InsertManyAsync(list);
                    break;
                case ApiOrderStatus.Sell:
                    await sellOrderCollection.InsertManyAsync(list);
                    break;
                case ApiOrderStatus.Sold:
                    await soldOrderCollection.InsertManyAsync(list);
                    break;
                case ApiOrderStatus.Canceled:
                    await canceledOrderCollection.InsertManyAsync(list);
                    break;
            }
        }
        public async Task InsertAsyncOrder(OrderData orderData, ApiOrderStatus status)
        {
            switch (status)
            {
                case ApiOrderStatus.Placed:
                    await placedOrderCollection.InsertOneAsync(orderData);
                    break;
                case ApiOrderStatus.MarketSold:
                    await marketOrderCollection.InsertOneAsync(orderData);
                    break;
                case ApiOrderStatus.Sell:
                    await sellOrderCollection.InsertOneAsync(orderData);
                    break;
                case ApiOrderStatus.Sold:
                    await soldOrderCollection.InsertOneAsync(orderData);
                    break;
                case ApiOrderStatus.Canceled:
                    await canceledOrderCollection.InsertOneAsync(orderData);
                    break;
            }
        }

        public async Task RemoveBulkAsyncOrders(List<OrderData> list, ApiOrderStatus status)
        {
            var builder = Builders<OrderData>.Filter;
            var filter = builder.In("ClientOrderId", list);
            switch (status)
            {
                case ApiOrderStatus.Placed:
                    await placedOrderCollection.DeleteManyAsync(filter);
                    break;
                case ApiOrderStatus.MarketSold:
                    await marketOrderCollection.DeleteManyAsync(filter);
                    break;
                case ApiOrderStatus.Sell:
                    await sellOrderCollection.DeleteManyAsync(filter);
                    break;
                case ApiOrderStatus.Sold:
                    await soldOrderCollection.DeleteManyAsync(filter);
                    break;
                case ApiOrderStatus.Canceled:
                    await canceledOrderCollection.DeleteManyAsync(filter);
                    break;
            }
        }

        public async Task RemoveAsyncOrder(OrderData orderData, ApiOrderStatus status)
        {
            var builder = Builders<OrderData>.Filter;
            var filter = builder.Eq("ClientOrderId", orderData.ClientOrderId);

            switch (status)
            {
                case ApiOrderStatus.Placed:
                    await placedOrderCollection.DeleteOneAsync(filter);
                    break;
                case ApiOrderStatus.MarketSold:
                    await marketOrderCollection.DeleteOneAsync(filter);
                    break;
                case ApiOrderStatus.Sell:
                    await sellOrderCollection.DeleteOneAsync(filter);
                    break;
                case ApiOrderStatus.Sold:
                    await soldOrderCollection.DeleteOneAsync(filter);
                    break;
                case ApiOrderStatus.Canceled:
                    await canceledOrderCollection.DeleteOneAsync(filter);
                    break;
            }
        }
        #endregion
    }
}
