// ReSharper disable InconsistentNaming
using System;
using System.Collections.Generic;
using System.Linq;

namespace Binance
{
    /// <summary>
    /// Defined assets (for convenience/reference only).
    /// </summary>
    public sealed class Asset : IComparable<Asset>, IEquatable<Asset>
    {
        #region Public Constants

        /// <summary>
        /// When the assets were last updated.
        /// </summary>
        public static readonly long LastUpdateAt = 1514924504970;

        // Redirect (BCH) Bitcoin Cash (BCC = BitConnect)
        public static readonly Asset BCH;

        public static readonly Asset ADA = new Asset("ADA", 8);
        public static readonly Asset ADX = new Asset("ADX", 8);
        public static readonly Asset AION = new Asset("AION", 8);
        public static readonly Asset AMB = new Asset("AMB", 8);
        public static readonly Asset ARK = new Asset("ARK", 8);
        public static readonly Asset ARN = new Asset("ARN", 8);
        public static readonly Asset AST = new Asset("AST", 8);
        public static readonly Asset BAT = new Asset("BAT", 8);
        public static readonly Asset BCC = new Asset("BCC", 8);
        public static readonly Asset BCD = new Asset("BCD", 8);
        public static readonly Asset BCPT = new Asset("BCPT", 8);
        public static readonly Asset BNB = new Asset("BNB", 8);
        public static readonly Asset BNT = new Asset("BNT", 8);
        public static readonly Asset BQX = new Asset("BQX", 8);
        public static readonly Asset BRD = new Asset("BRD", 8);
        public static readonly Asset BTC = new Asset("BTC", 8);
        public static readonly Asset BTG = new Asset("BTG", 8);
        public static readonly Asset BTS = new Asset("BTS", 8);
        public static readonly Asset CDT = new Asset("CDT", 8);
        public static readonly Asset CMT = new Asset("CMT", 8);
        public static readonly Asset CND = new Asset("CND", 8);
        public static readonly Asset DASH = new Asset("DASH", 8);
        public static readonly Asset DGD = new Asset("DGD", 8);
        public static readonly Asset DLT = new Asset("DLT", 8);
        public static readonly Asset DNT = new Asset("DNT", 8);
        public static readonly Asset EDO = new Asset("EDO", 8);
        public static readonly Asset ELF = new Asset("ELF", 8);
        public static readonly Asset ENG = new Asset("ENG", 8);
        public static readonly Asset ENJ = new Asset("ENJ", 8);
        public static readonly Asset EOS = new Asset("EOS", 8);
        public static readonly Asset ETC = new Asset("ETC", 8);
        public static readonly Asset ETH = new Asset("ETH", 8);
        public static readonly Asset EVX = new Asset("EVX", 8);
        public static readonly Asset FUEL = new Asset("FUEL", 8);
        public static readonly Asset FUN = new Asset("FUN", 8);
        public static readonly Asset GAS = new Asset("GAS", 8);
        public static readonly Asset GTO = new Asset("GTO", 8);
        public static readonly Asset GVT = new Asset("GVT", 8);
        public static readonly Asset GXS = new Asset("GXS", 8);
        public static readonly Asset HSR = new Asset("HSR", 8);
        public static readonly Asset ICN = new Asset("ICN", 8);
        public static readonly Asset ICX = new Asset("ICX", 8);
        public static readonly Asset IOTA = new Asset("IOTA", 8);
        public static readonly Asset KMD = new Asset("KMD", 8);
        public static readonly Asset KNC = new Asset("KNC", 8);
        public static readonly Asset LEND = new Asset("LEND", 8);
        public static readonly Asset LINK = new Asset("LINK", 8);
        public static readonly Asset LRC = new Asset("LRC", 8);
        public static readonly Asset LSK = new Asset("LSK", 8);
        public static readonly Asset LTC = new Asset("LTC", 8);
        public static readonly Asset LUN = new Asset("LUN", 8);
        public static readonly Asset MANA = new Asset("MANA", 8);
        public static readonly Asset MCO = new Asset("MCO", 8);
        public static readonly Asset MDA = new Asset("MDA", 8);
        public static readonly Asset MOD = new Asset("MOD", 8);
        public static readonly Asset MTH = new Asset("MTH", 8);
        public static readonly Asset MTL = new Asset("MTL", 8);
        public static readonly Asset NAV = new Asset("NAV", 8);
        public static readonly Asset NEBL = new Asset("NEBL", 8);
        public static readonly Asset NEO = new Asset("NEO", 8);
        public static readonly Asset NULS = new Asset("NULS", 8);
        public static readonly Asset OAX = new Asset("OAX", 8);
        public static readonly Asset OMG = new Asset("OMG", 8);
        public static readonly Asset OST = new Asset("OST", 8);
        public static readonly Asset POE = new Asset("POE", 8);
        public static readonly Asset POWR = new Asset("POWR", 8);
        public static readonly Asset PPT = new Asset("PPT", 8);
        public static readonly Asset QSP = new Asset("QSP", 8);
        public static readonly Asset QTUM = new Asset("QTUM", 8);
        public static readonly Asset RCN = new Asset("RCN", 8);
        public static readonly Asset RDN = new Asset("RDN", 8);
        public static readonly Asset REQ = new Asset("REQ", 8);
        public static readonly Asset SALT = new Asset("SALT", 8);
        public static readonly Asset SNGLS = new Asset("SNGLS", 8);
        public static readonly Asset SNM = new Asset("SNM", 8);
        public static readonly Asset SNT = new Asset("SNT", 8);
        public static readonly Asset STORJ = new Asset("STORJ", 8);
        public static readonly Asset STRAT = new Asset("STRAT", 8);
        public static readonly Asset SUB = new Asset("SUB", 8);
        public static readonly Asset TNB = new Asset("TNB", 8);
        public static readonly Asset TNT = new Asset("TNT", 8);
        public static readonly Asset TRIG = new Asset("TRIG", 8);
        public static readonly Asset TRX = new Asset("TRX", 8);
        public static readonly Asset USDT = new Asset("USDT", 8);
        public static readonly Asset VEN = new Asset("VEN", 8);
        public static readonly Asset VIB = new Asset("VIB", 8);
        public static readonly Asset WABI = new Asset("WABI", 8);
        public static readonly Asset WAVES = new Asset("WAVES", 8);
        public static readonly Asset WINGS = new Asset("WINGS", 8);
        public static readonly Asset WTC = new Asset("WTC", 8);
        public static readonly Asset XLM = new Asset("XLM", 8);
        public static readonly Asset XMR = new Asset("XMR", 8);
        public static readonly Asset XRP = new Asset("XRP", 8);
        public static readonly Asset XVG = new Asset("XVG", 8);
        public static readonly Asset XZC = new Asset("XZC", 8);
        public static readonly Asset YOYO = new Asset("YOYO", 8);
        public static readonly Asset ZEC = new Asset("ZEC", 8);
        public static readonly Asset ZRX = new Asset("ZRX", 8);

        #endregion Public Constants

        #region Implicit Operators

        public static bool operator ==(Asset x, Asset y) => Equals(x, y);

        public static bool operator !=(Asset x, Asset y) => !(x == y);

        public static implicit operator string(Asset asset) => asset.ToString();

        public static implicit operator Asset(string s)
        {
            var _s = s.FormatSymbol();
            lock (_sync)
            {
                return Cache.ContainsKey(_s) ? Cache[_s] : null;
            }
        }

        #endregion Implicit Operators

        #region Public Properties

        /// <summary>
        /// Asset cache.
        /// </summary>
        public static readonly IDictionary<string, Asset> Cache = new Dictionary<string, Asset>
        {
            { "ADA", ADA },
            { "ADX", ADX },
            { "AION", AION },
            { "AMB", AMB },
            { "ARK", ARK },
            { "ARN", ARN },
            { "AST", AST },
            { "BAT", BAT },
            { "BCC", BCC },
            { "BCD", BCD },
            { "BCPT", BCPT },
            { "BNB", BNB },
            { "BNT", BNT },
            { "BQX", BQX },
            { "BRD", BRD },
            { "BTC", BTC },
            { "BTG", BTG },
            { "BTS", BTS },
            { "CDT", CDT },
            { "CMT", CMT },
            { "CND", CND },
            { "DASH", DASH },
            { "DGD", DGD },
            { "DLT", DLT },
            { "DNT", DNT },
            { "EDO", EDO },
            { "ELF", ELF },
            { "ENG", ENG },
            { "ENJ", ENJ },
            { "EOS", EOS },
            { "ETC", ETC },
            { "ETH", ETH },
            { "EVX", EVX },
            { "FUEL", FUEL },
            { "FUN", FUN },
            { "GAS", GAS },
            { "GTO", GTO },
            { "GVT", GVT },
            { "GXS", GXS },
            { "HSR", HSR },
            { "ICN", ICN },
            { "ICX", ICX },
            { "IOTA", IOTA },
            { "KMD", KMD },
            { "KNC", KNC },
            { "LEND", LEND },
            { "LINK", LINK },
            { "LRC", LRC },
            { "LSK", LSK },
            { "LTC", LTC },
            { "LUN", LUN },
            { "MANA", MANA },
            { "MCO", MCO },
            { "MDA", MDA },
            { "MOD", MOD },
            { "MTH", MTH },
            { "MTL", MTL },
            { "NAV", NAV },
            { "NEBL", NEBL },
            { "NEO", NEO },
            { "NULS", NULS },
            { "OAX", OAX },
            { "OMG", OMG },
            { "OST", OST },
            { "POE", POE },
            { "POWR", POWR },
            { "PPT", PPT },
            { "QSP", QSP },
            { "QTUM", QTUM },
            { "RCN", RCN },
            { "RDN", RDN },
            { "REQ", REQ },
            { "SALT", SALT },
            { "SNGLS", SNGLS },
            { "SNM", SNM },
            { "SNT", SNT },
            { "STORJ", STORJ },
            { "STRAT", STRAT },
            { "SUB", SUB },
            { "TNB", TNB },
            { "TNT", TNT },
            { "TRIG", TRIG },
            { "TRX", TRX },
            { "USDT", USDT },
            { "VEN", VEN },
            { "VIB", VIB },
            { "WABI", WABI },
            { "WAVES", WAVES },
            { "WINGS", WINGS },
            { "WTC", WTC },
            { "XLM", XLM },
            { "XMR", XMR },
            { "XRP", XRP },
            { "XVG", XVG },
            { "XZC", XZC },
            { "YOYO", YOYO },
            { "ZEC", ZEC },
            { "ZRX", ZRX },
            
            // Redirect (BCH) Bitcoin Cash (BCC = BitConnect)
            { "BCH", BCC }
        };

        /// <summary>
        /// Get the asset symbol.
        /// </summary>
        public string Symbol { get; }

        /// <summary>
        /// Get the asset precision.
        /// </summary>
        public int Precision { get; }

        #endregion Public Properties

        #region Private Fields

        private static readonly object _sync = new object();

        #endregion Private Fields

        #region Constructors

        static Asset()
        {
            // Redirect (BCH) Bitcoin Cash (BCC = BitConnect)
            BCH = BCC;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="symbol">The asset symbol.</param>
        /// <param name="precision">The asset precision.</param>
        public Asset(string symbol, int precision)
        {
            if (string.IsNullOrWhiteSpace(symbol))
                throw new ArgumentNullException(nameof(symbol));

            if (precision <= 0)
                throw new ArgumentException("Asset precision must be greater than 0.", nameof(precision));

            Symbol = symbol.ToUpperInvariant();
            Precision = precision;
        }

        #endregion Constructors

        #region Public Methods

        /// <summary>
        /// Update the asset cache.
        /// </summary>
        /// <param name="symbols">The symbols.</param>
        /// <returns></returns>
        public static void UpdateCache(IEnumerable<Symbol> symbols)
        {
            Throw.IfNull(symbols, nameof(symbols));

            // ReSharper disable once PossibleMultipleEnumeration
            if (!symbols.Any())
                throw new ArgumentException("Enumerable must not be empty.", nameof(symbols));

            var assets = new List<Asset>();

            // ReSharper disable once PossibleMultipleEnumeration
            foreach (var symbol in symbols)
            {
                if (!assets.Contains(symbol.BaseAsset))
                    assets.Add(symbol.BaseAsset);

                if (!assets.Contains(symbol.QuoteAsset))
                    assets.Add(symbol.QuoteAsset);
            }

            lock (_sync)
            {
                // Remove any old assets (preserves redirections).
                foreach (var asset in Cache.Values.ToArray())
                {
                    if (!assets.Contains(asset))
                    {
                        Cache.Remove(asset);
                    }
                }

                // Update existing and add any new assets.
                foreach (var asset in assets)
                {
                    Cache[asset] = asset;
                }
            }
        }

        public override bool Equals(object obj)
        {
            return Symbol.Equals(obj);
        }

        public override int GetHashCode()
        {
            return Symbol.GetHashCode();
        }

        public override string ToString()
        {
            return Symbol;
        }

        #endregion Public Methods

        #region IComparable<Asset>

        public int CompareTo(Asset other)
        {
            return other == null ? 1 : string.Compare(Symbol, other.Symbol, StringComparison.Ordinal);
        }

        #endregion IComparable<Asset>

        #region IEquatable<Asset>

        public bool Equals(Asset other)
        {
            return CompareTo(other) == 0;
        }

        #endregion IEquatable<Asset>

        // File generated by BinanceCodeGenerator tool.
    }
}
