﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Binance.Api;
using Binance.Api.WebSocket;
using Binance.Api.WebSocket.Events;
using Binance.Cache.Events;
using Binance.Market;
using Microsoft.Extensions.Logging;

// ReSharper disable InconsistentlySynchronizedField

namespace Binance.Cache
{
    public sealed class CandlestickCache : WebSocketClientCache<ICandlestickWebSocketClient, CandlestickEventArgs, CandlestickCacheEventArgs>, ICandlestickCache
    {
        #region Public Properties

        public IEnumerable<Candlestick> Candlesticks
        {
            get { lock (_sync) { return _candlesticks?.ToArray() ?? new Candlestick[] { }; } }
        }

        #endregion Public Properties

        #region Private Fields

        private readonly List<Candlestick> _candlesticks;

        private readonly object _sync = new object();

        private string _symbol;
        private CandlestickInterval _interval;
        private int _limit;

        #endregion Private Fields

        #region Constructors

        public CandlestickCache(IBinanceApi api, ICandlestickWebSocketClient client, ILogger<CandlestickCache> logger = null)
            : base(api, client, logger)
        {
            _candlesticks = new List<Candlestick>();
        }

        #endregion Constructors

        #region Public Methods

        public async Task SubscribeAsync(string symbol, CandlestickInterval interval, int limit, Action<CandlestickCacheEventArgs> callback, CancellationToken token)
        {
            Throw.IfNullOrWhiteSpace(symbol, nameof(symbol));

            if (limit < 0)
                throw new ArgumentException($"{nameof(CandlestickCache)}: {nameof(limit)} must be greater than or equal to 0.", nameof(limit));

            if (!token.CanBeCanceled)
                throw new ArgumentException("Token must be capable of being in the canceled state.", nameof(token));

            token.ThrowIfCancellationRequested();

            _symbol = symbol.FormatSymbol();
            _interval = interval;
            _limit = limit;
            Token = token;

            LinkTo(Client, callback);

            try
            {
                await Client.SubscribeAsync(_symbol, interval, token)
                    .ConfigureAwait(false);
            }
            finally { UnLink(); }
        }

        public override void LinkTo(ICandlestickWebSocketClient client, Action<CandlestickCacheEventArgs> callback = null)
        {
            base.LinkTo(client, callback);
            Client.Candlestick += OnClientEvent;
        }

        public override void UnLink()
        {
            Client.Candlestick -= OnClientEvent;
            base.UnLink();
        }

        #endregion Public Methods

        #region Protected Methods

        protected override async Task<CandlestickCacheEventArgs> OnAction(CandlestickEventArgs @event)
        {
            if (_candlesticks.Count == 0)
            {
                await SynchronizeCandlesticksAsync(_symbol, _interval, _limit, Token)
                    .ConfigureAwait(false);
            }

            Logger?.LogDebug($"{nameof(CandlestickCache)}: Updating candlestick [open time: {@event.Candlestick.OpenTime}].");

            // Get the candlestick with matching open time.
            var candlestick = _candlesticks.FirstOrDefault(c => c.OpenTime == @event.Candlestick.OpenTime);

            lock (_sync)
            {
                _candlesticks.Remove(candlestick ?? _candlesticks.First());
                _candlesticks.Add(@event.Candlestick);
            }

            return new CandlestickCacheEventArgs(_candlesticks.ToArray());
        }

        #endregion Protected Methods

        #region Private Methods

        /// <summary>
        /// Get latest trades.
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="interval"></param>
        /// <param name="limit"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        private async Task SynchronizeCandlesticksAsync(string symbol, CandlestickInterval interval, int limit, CancellationToken token)
        {
            Logger?.LogInformation($"{nameof(CandlestickCache)}: Synchronizing candlesticks...");

            var candlesticks = await Api.GetCandlesticksAsync(symbol, interval, limit, token: token)
                .ConfigureAwait(false);

            lock (_sync)
            {
                _candlesticks.Clear();
                foreach (var candlestick in candlesticks)
                {
                    _candlesticks.Add(candlestick);
                }
            }
        }

        #endregion Private Methods
    }
}
