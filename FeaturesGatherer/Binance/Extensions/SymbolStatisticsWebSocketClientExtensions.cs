﻿using System.Threading;
using System.Threading.Tasks;

// ReSharper disable once CheckNamespace
namespace Binance.Api.WebSocket
{
    public static class SymbolStatisticsWebSocketClientExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="client"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static Task SubscribeAsync(this ISymbolStatisticsWebSocketClient client, CancellationToken token)
            => client.SubscribeAsync(null, token);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="client"></param>
        /// <param name="symbol"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static Task SubscribeAsync(this ISymbolStatisticsWebSocketClient client, string symbol, CancellationToken token)
            => client.SubscribeAsync(symbol, null, token);
    }
}
