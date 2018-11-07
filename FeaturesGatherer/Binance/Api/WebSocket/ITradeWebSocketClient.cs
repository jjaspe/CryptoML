﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Binance.Api.WebSocket.Events;

namespace Binance.Api.WebSocket
{
    public interface ITradeWebSocketClient : IBinanceWebSocketClient
    {
        #region Public Events

        /// <summary>
        /// The trade event.
        /// </summary>
        event EventHandler<TradeEventArgs> Trade;

        #endregion Public Events

        #region Public Methods

        /// <summary>
        /// Subscribe to the specified symbol and begin receiving trade events.
        /// Awaiting this method will not return until the token
        /// is canceled, this <see cref="ITradeWebSocketClient"/> is disposed,
        /// or an exception occurs.
        /// </summary>
        /// <param name="symbol">The symbol.</param>
        /// <param name="callback">An event callback.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns><see cref="Task"/></returns>
        Task SubscribeAsync(string symbol, Action<TradeEventArgs> callback, CancellationToken token);

        #endregion Public Methods
    }
}
