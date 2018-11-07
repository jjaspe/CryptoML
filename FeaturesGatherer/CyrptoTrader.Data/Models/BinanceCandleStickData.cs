using CyrptoTrader.Models;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace CyrptoTrader.Data
{
    public class BinanceCandleStickData
    {
        public BinanceCandleStickData(BinanceCandleStick stick)
        {
            this.OpenTime = stick.OpenTime;
            this.Open = stick.Open;
            this.High = stick.High;
            this.Low = stick.Low;
            this.Close = stick.Close;
            this.Volume = stick.Volume;
            this.CloseTime = stick.CloseTime;
            this.QuoteAssetVolume = stick.QuoteAssetVolume;
            this.NumberOfTradesDuringPeriod = stick.NumberOfTradesDuringPeriod;
            this.IsClosed = stick.IsClosed;
            this.Change = stick.Change;
            this.AvgGain = stick.AvgGain;
            this.AvgLoss = stick.AvgLoss;
            this.VolumeMA = stick.VolumeMA;
            this.MAs = stick.MAs;
            this.BollingerBands = stick.BollingerBands;
        }

        [BsonId(IdGenerator = typeof(CollectionIdGenerator))]
        public string Id { get; set; }
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
    }
}
