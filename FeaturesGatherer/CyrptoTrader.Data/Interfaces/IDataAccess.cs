using CyrptoTrader.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CyrptoTrader.Data
{
    public interface IDataAccess
    {
        Task<List<BinanceSymbolData>> GetSymbolData();
        Task InsertBulkAsyncSymbolData(List<BinanceSymbolData> list);
        Task DeleteBulkAsyncSymbolData();
        Task InsertAsyncInProgressBinanceOrder(BinanceOrderData binanceOrder);
        Task InsertAsyncCompletedBinanceOrders(BinanceOrderData binanceOrder);
        Task<List<BinanceOrderData>> GetBulkAsyncBinanceOrders(BinanceOrderStatus status);
        Task RemoveAsyncInProgressBinanceOrder(BinanceOrderData binanceOrder);
        Task RemoveAsyncCompletedBinanceOrder(BinanceOrderData binanceOrder);
        Task<List<OrderData>> GetBulkAsyncOrder(ApiOrderStatus status);
        Task InsertBulkAsyncOrders(List<OrderData> list, ApiOrderStatus status);
        Task InsertAsyncOrder(OrderData orderData, ApiOrderStatus status);
        Task RemoveBulkAsyncOrders(List<OrderData> list, ApiOrderStatus status);
        Task RemoveAsyncOrder(OrderData orderData, ApiOrderStatus status);
    }
}
