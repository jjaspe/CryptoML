using CyrptoTrader.Data;
using CyrptoTrader.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FeaturesGatherer.Services
{
    public class FeatureWriterService : IFeatureWriterService
    {
        IDataAccess dataAccess;
        IIndicatorService indicatorService;

        public FeatureWriterService(IDataAccess dataAccess,
            IIndicatorService indicatorService)
        {
            this.dataAccess = dataAccess;
            this.indicatorService = indicatorService;
        }

        public void WriteTrainingData(string featuresFilename, string resultsFilename
            ,string verificationFeaturesFile, string verificationResultsFile)
        {
            var buckets = SetupBuckets();

            var trainingData = GetDataLists();
            WriteFeaturesFile(featuresFilename, trainingData);
            WriteResultsFile(resultsFilename, trainingData, buckets);

            var verificationData = GetDataLists(false);
            WriteFeaturesFile(verificationFeaturesFile, verificationData);
            WriteResultsFile(verificationResultsFile, verificationData, buckets);
        }

        private List<BinanceCandleStick> GetDataLists(bool training = true)
        {
            List<BinanceCandleStick> datasets = new List<BinanceCandleStick>();
            List<BinanceSymbolData> lists = dataAccess.GetSymbolData().GetAwaiter().GetResult();
            foreach(var list in lists)
            {
                var periods = list.ThirtyMinPeriods.OrderBy(n => n.OpenTime)
                    .Take(612).Reverse()
                    .Select(n => GetBinanceCandleStick(n)).ToList();
                indicatorService.PopulateSymbolDataListMetaData(periods);
                var current = new BinanceSymbol()
                {
                    AssetName = list.AssetName,
                    Periods = periods,
                    Name = list.Name
                };

                datasets.AddRange(training?
                    GetTrainingDataSetsForSymbol(current)
                    :GetVerificationDataSetsForSymbol(current));
            }

            return datasets;
        }

        private List<BinanceCandleStick> GetTrainingDataSetsForSymbol(BinanceSymbol symbol)
        {
            List<BinanceCandleStick> datasets = new List<BinanceCandleStick>();
            for(int i = 100; i<symbol.Periods.Count && i < 200; i++)
            {
                datasets.Add(symbol.Periods[i]);
            }
            return datasets;
        }

        private List<BinanceCandleStick> GetVerificationDataSetsForSymbol(BinanceSymbol symbol)
        {
            List<BinanceCandleStick> datasets = new List<BinanceCandleStick>();
            for (int i = 200; i < symbol.Periods.Count && i < 600; i++)
            {
                datasets.Add(symbol.Periods[i]);
            }
            return datasets;
        }

        private BinanceCandleStick GetBinanceCandleStick(BinanceCandleStickData stick)
        {
            BinanceCandleStick bcs = new BinanceCandleStick();
            bcs.OpenTime = stick.OpenTime;
            bcs.Open = stick.Open;
            bcs.High = stick.High;
            bcs.Low = stick.Low;
            bcs.Close = stick.Close;
            bcs.Volume = stick.Volume;
            bcs.CloseTime = stick.CloseTime;
            bcs.QuoteAssetVolume = stick.QuoteAssetVolume;
            bcs.NumberOfTradesDuringPeriod = stick.NumberOfTradesDuringPeriod;
            bcs.IsClosed = stick.IsClosed;
            bcs.Change = stick.Change;
            bcs.AvgGain = stick.AvgGain;
            bcs.AvgLoss = stick.AvgLoss;
            bcs.VolumeMA = stick.VolumeMA;
            bcs.MAs = stick.MAs;
            bcs.BollingerBands = stick.BollingerBands;
            return bcs;
        }

        private void WriteFeaturesFile(string filename, List<BinanceCandleStick> datasets)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < datasets.Count - 2 * 6; i++)
            {
                var set = datasets[i];
                if (set.VolumeMA.HasValue && set.RSI.HasValue)
                {
                    builder.Append(ToFormattedString(set.RSI.Value));
                    builder.Append(",");
                    builder.Append(ToFormattedString(set.MAs[0].Average));
                    builder.Append(",");
                    builder.Append(ToFormattedString(set.MAs[1].Average));
                    builder.Append(",");
                    builder.Append(ToFormattedString(set.MAs[2].Average));
                    builder.Append(",");
                    builder.Append(ToFormattedString(set.BollingerBands.Upper));
                    builder.Append(",");
                    builder.Append(ToFormattedString(set.VolumeMA.Value));
                    builder.Append(",");
                    builder.Append(ToFormattedString(set.Prev1Delta));
                    builder.Append(",");
                    builder.Append(ToFormattedString(set.Prev2Delta));
                    builder.Append(",");
                    builder.Append(ToFormattedString(set.Prev3Delta));
                    builder.Append("\r\n");
                }
            }
            File.WriteAllText(filename, builder.ToString());
        }

        public void WriteResultsFile(string filename, List<BinanceCandleStick> datasets, List<decimal> buckets)
        {
            datasets = PopulateBuckets(CalculateHighsAndLowsPerInterval(datasets), buckets);
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < datasets.Count - 2 * 6; i++)
            {
                var set = datasets[i];
                builder.Append(ToFormattedString(set.MaxBucket));
                builder.Append(",");
                builder.Append(ToFormattedString(set.MinBucket));
                builder.Append("\r\n");
            }
            File.WriteAllText(filename, builder.ToString());
        }

        /// <summary>
        /// Setup result regions, i.e. discrete intervals where prices might end up, as 
        /// a percent of starting close, after 6 hours.
        /// </summary>
        /// <returns></returns>
        private List<decimal> SetupBuckets()
        {
            var buckets = Constants.OutputClasses;
            List<decimal> bucketList = new List<decimal>();
            for (decimal i = buckets.Start; i < buckets.End; i+=buckets.Step)
            {
                bucketList.Add(i);
            }
            return bucketList;
        }

        /// <summary>
        /// Calculates the lowest and highest of the possible result regions (buckets) for each
        /// interval, based on their highest and lowest value. E.g. if you have buckets from -5 to 5, 
        /// in intervals of 1, and a set has lowest -2.3 and highest 3.8, it's lowest bucket is -2 and highest
        /// is 3.
        /// </summary>
        /// <param name="datasets"></param>
        /// <param name="buckets"></param>
        /// <returns></returns>
        private List<BinanceCandleStick> PopulateBuckets(List<BinanceCandleStick> datasets,
            List<decimal> buckets)
        {
            for (int i = 0; i < datasets.Count - 2 * 6; i++)
            {
                var set = datasets[i];
                var higher = buckets.Where(n => n < set.PeriodHigh);
                var lower = buckets.Where(n => n > set.PeriodLow);
                set.MaxBucket = higher.Count() > 0? higher.Max() : set.PeriodHigh;
                set.MinBucket = lower.Count() > 0? lower.Min() : set.PeriodLow;
            }
            return datasets;
        }

        /// <summary>
        /// Calculates highs and lows for every 6 hour interval, given as a percent of closing time
        /// at the start of interval. E.g. if coin goes from 10, to 7, to 15, the stick for the beginning of that interval
        /// will have -30 as PeriodLow, 50 as Period high
        /// </summary>
        /// <param name="datasets"></param>
        /// <returns></returns>
        private List<BinanceCandleStick> CalculateHighsAndLowsPerInterval(List<BinanceCandleStick> datasets)
        {
            //dont do last 6 hours worth of intervals, since they wont have 6 hours worth of data after them
            for (int i = 0; i < datasets.Count - 2*6; i++)
            {
                var nextPeriodsClose = datasets.Skip(i).Take(12).Select(n => n.Close);
                datasets[i].PeriodHigh = (nextPeriodsClose.Max() - datasets[i].Close)
                    / datasets[i].Close*100;
                datasets[i].PeriodLow = (nextPeriodsClose.Min() - datasets[i].Close)
                    / datasets[i].Close * 100;
            }
            return datasets;
        }

        private string ToFormattedString(decimal d)
        {
            string s = d.ToString();
            return s.Substring(0, Math.Min(s.Length, 10));
        }
    }
}
