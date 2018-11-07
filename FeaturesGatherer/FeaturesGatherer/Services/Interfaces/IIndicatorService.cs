using System.Collections.Generic;
using CyrptoTrader.Models;

namespace FeaturesGatherer.Services
{
    public interface IIndicatorService
    {
        decimal CalculateAvgGain(List<BinanceCandleStick> periods, int periodInterval);
        decimal CalculateAvgLoss(List<BinanceCandleStick> periods, int periodInterval);
        decimal CalculateChange(BinanceCandleStick periodToBeCalculated, BinanceCandleStick previousPeriod);
        void PopulateSymbolDataListMetaData(List<BinanceCandleStick> binanceStickData);
    }
}