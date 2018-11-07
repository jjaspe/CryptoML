using Binance.Market;
using System;
using System.Collections.Generic;
using System.Text;

namespace CyrptoTrader.Models
{
    public class BinanceCandleStick
    {
        public BinanceCandleStick() { }

        public BinanceCandleStick(Candlestick candleStick)
        {
            OpenTime = candleStick.OpenTime;
            Open = candleStick.Open;
            High = candleStick.High;
            Low = candleStick.Low;
            Close = candleStick.Close;
            Volume = candleStick.Volume;
            CloseTime = candleStick.CloseTime;
            QuoteAssetVolume = candleStick.QuoteAssetVolume;
            NumberOfTradesDuringPeriod = candleStick.NumberOfTrades;
            MAs = new List<MovingAverage>();
            BollingerBands = new BollingerBand();
        }

        #region Fields
        public long OpenTime { get; set; }
        public decimal Open { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public decimal Close { get; set; }
        public decimal Volume { get; set; }
        public long CloseTime { get; set; }
        public decimal QuoteAssetVolume { get; set; }
        public long NumberOfTradesDuringPeriod { get; set; }
        public bool IsClosed { get; set; }
        public decimal Change { get; set; }
        public decimal? AvgGain { get; set; }
        public decimal? AvgLoss { get; set; }
        public decimal? VolumeMA { get; set; }
        public List<MovingAverage> MAs { get; set; }
        public BollingerBand BollingerBands { get; set; }
        #endregion

        #region ML result values
        public decimal PeriodHigh { get; set; }
        public decimal PeriodLow { get; set; }
        public decimal MaxBucket { get; set; }
        public decimal MinBucket { get; set; }
        public decimal Prev1Delta { get; set; }
        public decimal Prev2Delta { get; set; }
        public decimal Prev3Delta { get; set; }
        #endregion

        #region Calculated Properties
        public decimal? RSI
        {
            get
            {
                if (RS.HasValue)
                    return Math.Round((decimal)(100 - (100 / (1 + RS))), 4);
                else
                    return null;
            }
        }

        public decimal? RS
        {
            get
            {
                if (AvgGain.HasValue && AvgLoss.HasValue)
                    return AvgGain.Value / AvgLoss.Value;
                else
                    return null;
            }
        }

        public decimal Gain
        {
            get
            {
                if (Change > 0)
                    return Change;
                else
                    return 0;
            }
        }
        public decimal Loss
        {
            get
            {
                if (Change < 0)
                    return Math.Abs(Change);
                else
                    return 0;
            }
        }

        public bool IsEven
        {
            get { return (Open == Close); }
        }

        public bool IsPositive
        {
            get { return (Open < Close); }
        }

        public bool IsNegative
        {
            get { return (Open > Close); }
        }

        public decimal TopWickLength
        {
            get
            {
                if (IsNegative)
                    return High - Open;
                else
                    return High - Close;
            }
        }

        public decimal BottomWickLength
        {
            get
            {
                if (IsNegative)
                    return Close - Low;
                else
                    return Open - Low;
            }
        }

        public decimal Body
        {
            get { return Math.Abs(Close - Open); }
        }

        public decimal AmountOfBitcoinTraded
        {
            get { return Math.Abs((Open + Close)) / 2 * Volume; }
        }
        #endregion
    }
}
