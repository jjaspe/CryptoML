﻿using Binance.Api.WebSocket;

// ReSharper disable once CheckNamespace
namespace Binance
{
    public sealed class UserDataWebSocketClientOptions
    {
        /// <summary>
        /// Keep-alive timer period.
        /// </summary>
        public int KeepAliveTimerPeriod { get; set; } = UserDataWebSocketClient.KeepAliveTimerPeriodDefault;
    }
}
