using Binance;
using Binance.Account.Orders;
using CyrptoTrader.Models.Enums;
using CyrptoTrader.Models.Patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CyrptoTrader.Models
{
    public static class Constants
    {
        public static Settings Settings { get; set; } = new Settings();

        //TEMPORARY
        public static int TotalBuysThisSession { get; set; }
        public static int TotalSellsThisSession { get; set; }
        public static int TotalCanceledThisSession { get; set; }
        public static decimal TotalProfitInBitcoinThisSession { get; set; }

        public static int InsufficientFundPatternsThisSession = 0;
        public static List<BinanceOrder> MyOrders { get; set; } = new List<BinanceOrder>();
        public static List<Order> SellOrders { get; set; } = new List<Order>();
        public static List<Order> SoldOrders { get; set; } = new List<Order>();
        public static List<Order> FilledBuyOrders { get; set; } = new List<Order>();
        public static List<Order> PendingCanceledOrders { get; set; } = new List<Order>();
        public static List<Order> CanceledOrders { get; set; } = new List<Order>();
        public static List<Order> NewlyPlacedOrders { get; set; } = new List<Order>();
        public static long AppStartedTimestamp { get; set; }
        public static int Interval = 30;

        public static DateTime UnixTimeStampToDateTime(long unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddMilliseconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }

        public static DateTime UnixTimeStampToLocalTime(long unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddMilliseconds(unixTimeStamp).ToUniversalTime();
            dtDateTime = dtDateTime.ToLocalTime();
            return dtDateTime;
        }

        public static long GetCurrentTimeStamp()
        {
            return (long)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalMilliseconds;
        }

        public static List<Symbol> BuildUsedSymbolList(bool hardcoded = false, SymbolGroups group = Enums.SymbolGroups.Low)
        {
            if (hardcoded || SymbolGroups.Count == 0)
                return HardcodedList();
            else
            {
                switch (group)
                {
                    case Enums.SymbolGroups.Low:
                        return SymbolGroups[0].Symbols;
                    case Enums.SymbolGroups.Middle:
                        return GetSymbolRange(1, SymbolGroups.Count / 2);
                    case Enums.SymbolGroups.High:
                        return GetSymbolRange(SymbolGroups.Count / 2, SymbolGroups.Count);
                    default:
                        return SymbolGroups[0].Symbols;
                }
            }                
        }

        /// <summary>
        /// Inclusive on start, exclusive on end
        /// </summary>
        public static List<Symbol> GetSymbolRange(int start, int end)
        {
            var symbols = new List<Symbol>();
            if (SymbolGroups.Count > 0)
            {
                for (int i = start; i < end; i++)
                {
                    symbols.AddRange(SymbolGroups[i].Symbols);
                }
            }
            return symbols;
        }

        static List<Symbol> HardcodedList()
        {
            List<Symbol> scanSymbols = new List<Symbol>();
            scanSymbols.Add(Symbol.ADA_BTC);
            scanSymbols.Add(Symbol.ADX_BTC);
            scanSymbols.Add(Symbol.ARN_BTC);
            scanSymbols.Add(Symbol.AST_BTC);
            scanSymbols.Add(Symbol.BAT_BTC);
            scanSymbols.Add(Symbol.BQX_BTC);
            scanSymbols.Add(Symbol.BRD_BTC);
            scanSymbols.Add(Symbol.BTS_BTC);
            scanSymbols.Add(Symbol.CDT_BTC);
            scanSymbols.Add(Symbol.CMT_BTC);
            scanSymbols.Add(Symbol.CND_BTC);
            scanSymbols.Add(Symbol.DNT_BTC);
            scanSymbols.Add(Symbol.ELF_BTC);
            scanSymbols.Add(Symbol.ENG_BTC);
            scanSymbols.Add(Symbol.ENJ_BTC);
            scanSymbols.Add(Symbol.EVX_BTC);
            scanSymbols.Add(Symbol.FUEL_BTC);
            scanSymbols.Add(Symbol.FUN_BTC);
            scanSymbols.Add(Symbol.GTO_BTC);
            scanSymbols.Add(Symbol.IOTA_BTC);
            scanSymbols.Add(Symbol.KNC_BTC);
            scanSymbols.Add(Symbol.LEND_BTC);
            scanSymbols.Add(Symbol.LINK_BTC);
            scanSymbols.Add(Symbol.LRC_BTC);
            scanSymbols.Add(Symbol.MANA_BTC);
            scanSymbols.Add(Symbol.MTH_BTC);
            scanSymbols.Add(Symbol.OST_BTC);
            scanSymbols.Add(Symbol.POE_BTC);
            scanSymbols.Add(Symbol.POWR_BTC);
            scanSymbols.Add(Symbol.QSP_BTC);
            scanSymbols.Add(Symbol.REQ_BTC);
            scanSymbols.Add(Symbol.SNGLS_BTC);
            scanSymbols.Add(Symbol.SNT_BTC);
            scanSymbols.Add(Symbol.STORJ_BTC);
            scanSymbols.Add(Symbol.SUB_BTC);
            scanSymbols.Add(Symbol.TNB_BTC);
            scanSymbols.Add(Symbol.TRX_BTC);
            scanSymbols.Add(Symbol.VEN_BTC);
            scanSymbols.Add(Symbol.VIB_BTC);
            scanSymbols.Add(Symbol.WABI_BTC);
            scanSymbols.Add(Symbol.XLM_BTC);
            scanSymbols.Add(Symbol.XVG_BTC);
            scanSymbols.Add(Symbol.XRP_BTC);
            scanSymbols.Add(Symbol.YOYO_BTC);
            scanSymbols.Add(Symbol.ZRX_BTC);

            return scanSymbols;
        }

        public static List<Symbol> BuildFullSymbolList()
        {
            List<Symbol> scanSymbols = new List<Symbol>();
            scanSymbols.Add(Symbol.ADA_BTC);
            scanSymbols.Add(Symbol.ADX_BTC);
            scanSymbols.Add(Symbol.AION_BTC);
            scanSymbols.Add(Symbol.AMB_BTC);
            scanSymbols.Add(Symbol.ARK_BTC);
            scanSymbols.Add(Symbol.ARN_BTC);
            scanSymbols.Add(Symbol.AST_BTC);
            scanSymbols.Add(Symbol.BAT_BTC);
            scanSymbols.Add(Symbol.BCC_BTC);
            scanSymbols.Add(Symbol.BCD_BTC);
            scanSymbols.Add(Symbol.BCPT_BTC);
            scanSymbols.Add(Symbol.BNB_BTC);
            scanSymbols.Add(Symbol.BNT_BTC);
            scanSymbols.Add(Symbol.BQX_BTC);
            scanSymbols.Add(Symbol.BRD_BTC);
            scanSymbols.Add(Symbol.BTG_BTC);
            scanSymbols.Add(Symbol.BTS_BTC);
            scanSymbols.Add(Symbol.CDT_BTC);
            scanSymbols.Add(Symbol.CMT_BTC);
            scanSymbols.Add(Symbol.CND_BTC);
            scanSymbols.Add(Symbol.DASH_BTC);
            scanSymbols.Add(Symbol.DGD_BTC);
            scanSymbols.Add(Symbol.DLT_BTC);
            scanSymbols.Add(Symbol.DNT_BTC);
            scanSymbols.Add(Symbol.EDO_BTC);
            scanSymbols.Add(Symbol.ELF_BTC);
            scanSymbols.Add(Symbol.ENG_BTC);
            scanSymbols.Add(Symbol.ENJ_BTC);
            scanSymbols.Add(Symbol.EOS_BTC);
            scanSymbols.Add(Symbol.ETC_BTC);
            scanSymbols.Add(Symbol.ETH_BTC);
            scanSymbols.Add(Symbol.EVX_BTC);
            scanSymbols.Add(Symbol.FUEL_BTC);
            scanSymbols.Add(Symbol.FUN_BTC);
            scanSymbols.Add(Symbol.GAS_BTC);
            scanSymbols.Add(Symbol.GTO_BTC);
            scanSymbols.Add(Symbol.GVT_BTC);
            scanSymbols.Add(Symbol.GXS_BTC);
            scanSymbols.Add(Symbol.HSR_BTC);
            scanSymbols.Add(Symbol.ICN_BTC);
            scanSymbols.Add(Symbol.ICX_BTC);
            scanSymbols.Add(Symbol.IOTA_BTC);
            scanSymbols.Add(Symbol.KMD_BTC);
            scanSymbols.Add(Symbol.KNC_BTC);
            scanSymbols.Add(Symbol.LEND_BTC);
            scanSymbols.Add(Symbol.LINK_BTC);
            scanSymbols.Add(Symbol.LRC_BTC);
            scanSymbols.Add(Symbol.LSK_BTC);
            scanSymbols.Add(Symbol.LTC_BTC);
            scanSymbols.Add(Symbol.LUN_BTC);
            scanSymbols.Add(Symbol.MANA_BTC);
            scanSymbols.Add(Symbol.MCO_BTC);
            scanSymbols.Add(Symbol.MDA_BTC);
            scanSymbols.Add(Symbol.MOD_BTC);
            scanSymbols.Add(Symbol.MTH_BTC);
            scanSymbols.Add(Symbol.MTL_BTC);
            scanSymbols.Add(Symbol.NAV_BTC);
            scanSymbols.Add(Symbol.NEBL_BTC);
            scanSymbols.Add(Symbol.NEO_BTC);
            scanSymbols.Add(Symbol.NULS_BTC);
            scanSymbols.Add(Symbol.OAX_BTC);
            scanSymbols.Add(Symbol.OMG_BTC);
            scanSymbols.Add(Symbol.OST_BTC);
            scanSymbols.Add(Symbol.POE_BTC);
            scanSymbols.Add(Symbol.POWR_BTC);
            scanSymbols.Add(Symbol.PPT_BTC);
            scanSymbols.Add(Symbol.QSP_BTC);
            scanSymbols.Add(Symbol.QTUM_BTC);
            scanSymbols.Add(Symbol.RCN_BTC);
            scanSymbols.Add(Symbol.RDN_BTC);
            scanSymbols.Add(Symbol.REQ_BTC);
            scanSymbols.Add(Symbol.SALT_BTC);
            scanSymbols.Add(Symbol.SNGLS_BTC);
            scanSymbols.Add(Symbol.SNM_BTC);
            scanSymbols.Add(Symbol.SNT_BTC);
            scanSymbols.Add(Symbol.STORJ_BTC);
            scanSymbols.Add(Symbol.STRAT_BTC);
            scanSymbols.Add(Symbol.SUB_BTC);
            scanSymbols.Add(Symbol.TNB_BTC);
            scanSymbols.Add(Symbol.TNT_BTC);
            scanSymbols.Add(Symbol.TRIG_BTC);
            scanSymbols.Add(Symbol.TRX_BTC);
            scanSymbols.Add(Symbol.VEN_BTC);
            scanSymbols.Add(Symbol.VIB_BTC);
            scanSymbols.Add(Symbol.WABI_BTC);
            scanSymbols.Add(Symbol.WAVES_BTC);
            scanSymbols.Add(Symbol.WINGS_BTC);
            scanSymbols.Add(Symbol.WTC_BTC);
            scanSymbols.Add(Symbol.XLM_BTC);
            scanSymbols.Add(Symbol.XMR_BTC);
            scanSymbols.Add(Symbol.XRP_BTC);
            scanSymbols.Add(Symbol.XVG_BTC);
            scanSymbols.Add(Symbol.XZC_BTC);
            scanSymbols.Add(Symbol.YOYO_BTC);
            scanSymbols.Add(Symbol.ZEC_BTC);
            scanSymbols.Add(Symbol.ZRX_BTC);
            return scanSymbols;
        }

        public static List<PatternGrouping> SymbolGroups { get; set; } = new List<PatternGrouping>();
        public static List<Pattern> AllPatterns { get; set; } = new List<Pattern>();
        public static List<SubRSIPattern> RSIPatterns { get; set; } = new List<SubRSIPattern>();
        public static List<DropOffPattern> DropPatterns { get; set; } = new List<DropOffPattern>();
        public static List<InBetweenPattern> InBetweenPatterns { get; set; } = new List<InBetweenPattern>();
        public static List<BollingerBandPattern> BollingerBandPatterns { get; set; } = new List<BollingerBandPattern>();
    }
}
