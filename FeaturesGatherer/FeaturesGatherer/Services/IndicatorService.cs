using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CyrptoTrader.Models;

namespace FeaturesGatherer.Services
{
    public class IndicatorService : IIndicatorService
    {
        public void PopulateSymbolDataListMetaData(List<BinanceCandleStick> binanceStickData)
        {
            List<int> movingAverageLengths = new List<int>() { 7, 25, 99 };
            CalculateRSIForFreshData(binanceStickData);
            PopulateMovingAverageForFreshData(binanceStickData, movingAverageLengths);
            CalculateVolumeMA(binanceStickData);
            CalculateBollingerBands(binanceStickData);
            CalculatePreviousPeriodsDeltas(binanceStickData);
        }

        private void CalculatePreviousPeriodsDeltas(List<BinanceCandleStick> binanceStickData)
        {
            for (int i = 3; i < binanceStickData.Count; i++)
            {
                binanceStickData[i].Prev1Delta = (binanceStickData[i].Close - binanceStickData[i - 1].Close) /
                    binanceStickData[i].Close * 100;

                binanceStickData[i].Prev2Delta = (binanceStickData[i].Close - binanceStickData[i - 2].Close) /
                    binanceStickData[i].Close * 100;

                binanceStickData[i].Prev3Delta = (binanceStickData[i].Close - binanceStickData[i - 3].Close) /
                    binanceStickData[i].Close * 100;
            }
        }

        public decimal CalculateChange(BinanceCandleStick periodToBeCalculated, BinanceCandleStick previousPeriod)
        {
            return periodToBeCalculated.Close - previousPeriod.Close;
        }

        public decimal CalculateAvgGain(List<BinanceCandleStick> periods, int periodInterval)
        {
            var previousPeriod = periods[periods.Count - 2];
            var currentPeriod = periods.Last();

            if (previousPeriod.AvgGain == null)
                return periods.Sum(x => x.Gain) / periodInterval;
            else
                return (decimal)((previousPeriod.AvgGain * 13 + currentPeriod.Gain) / periodInterval);
        }
        public decimal CalculateAvgLoss(List<BinanceCandleStick> periods, int periodInterval)
        {
            var previousPeriod = periods[periods.Count - 2];
            var currentPeriod = periods.Last();
            if (previousPeriod.AvgLoss == null)
                return periods.Sum(x => x.Loss) / periodInterval;
            else
                return (decimal)((previousPeriod.AvgLoss * 13 + currentPeriod.Loss) / periodInterval);
        }

        private void CalculateRSIForFreshData(List<BinanceCandleStick> binanceStickData)
        {
            int rsiPeriodInterval = 14;
            List<BinanceCandleStick> currentCalculationSet = new List<BinanceCandleStick>();
            for (int i = 0; i < binanceStickData.Count; i++)
            {
                if (i > 0)
                    binanceStickData[i].Change = CalculateChange(binanceStickData[i], binanceStickData[i - 1]);
                else
                    binanceStickData[i].Change = 0;
                currentCalculationSet.Add(binanceStickData[i]);

                if (currentCalculationSet.Count >= rsiPeriodInterval)
                {
                    binanceStickData[i].AvgGain = CalculateAvgGain(currentCalculationSet, rsiPeriodInterval);
                    binanceStickData[i].AvgLoss = CalculateAvgLoss(currentCalculationSet, rsiPeriodInterval);
                    currentCalculationSet.RemoveAt(0);
                }
            }
        }

        private void PopulateMovingAverageForFreshData(List<BinanceCandleStick> periods, List<int> lengths)
        {
            List<BinanceCandleStick> calculationSet = new List<BinanceCandleStick>();
            foreach (var length in lengths)
            {
                for (int i = 0; i < periods.Count; i++)
                {
                    calculationSet.Add(periods[i]);
                    if (i >= length - 1)
                    {
                        periods[i].MAs.Add(CalculateMovingAverage(calculationSet, length));
                        calculationSet.RemoveAt(0);
                    }
                }
            }
        }

        private void CalculateBollingerBands(List<BinanceCandleStick> periods)
        {
            List<BinanceCandleStick> calculationSet = new List<BinanceCandleStick>();
            int length = 20;
            for (int i = 0; i < periods.Count; i++)
            {
                calculationSet.Add(periods[i]);
                if (i >= length - 1)
                {
                    decimal TwentyPeriodSdOfPriceTimesTwo = (decimal)CalculateStandardDeviationOfPrice(calculationSet) * 2;
                    periods[i].BollingerBands.Middle = calculationSet.Sum(x => x.Close) / length;
                    periods[i].BollingerBands.Upper = periods[i].BollingerBands.Middle + TwentyPeriodSdOfPriceTimesTwo;
                    periods[i].BollingerBands.Lower = periods[i].BollingerBands.Middle - TwentyPeriodSdOfPriceTimesTwo;
                    calculationSet.RemoveAt(0);
                }
            }
        }

        private double CalculateStandardDeviationOfPrice(List<BinanceCandleStick> periods)
        {
            decimal closeAverage = periods.Average(x => x.Close);
            decimal sumOfSquaresOfDifferences = periods.Select(x => (x.Close - closeAverage) * (x.Close - closeAverage)).Sum();
            double sd = Math.Sqrt((double)sumOfSquaresOfDifferences / periods.Count);

            return sd;
        }

        private MovingAverage CalculateMovingAverage(List<BinanceCandleStick> periods, int length)
        {
            return new MovingAverage()
            {
                Length = length,
                Average = periods.Average(x => x.Close)
            };
        }

        private void CalculateVolumeMA(List<BinanceCandleStick> binanceStickData)
        {
            int volumeMAInterval = 20;
            List<BinanceCandleStick> volumeMaCalculationSet = new List<BinanceCandleStick>();
            for (int i = 0; i < binanceStickData.Count; i++)
            {
                volumeMaCalculationSet.Add(binanceStickData[i]);

                if (volumeMaCalculationSet.Count >= volumeMAInterval)
                {
                    binanceStickData[i].VolumeMA = (volumeMaCalculationSet.Sum(x => x.Volume) / volumeMAInterval);
                    volumeMaCalculationSet.RemoveAt(0);
                }
            }
        }
    }
}
