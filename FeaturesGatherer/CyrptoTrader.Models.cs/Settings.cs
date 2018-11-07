using System;
using System.Collections.Generic;
using System.Text;

namespace CyrptoTrader.Models
{
    public class Settings
    {
        public int TimeTillCancel { get; set; }
        public decimal CommissionFee { get; set; }
        public string APIKey { get; set; }
        public string Secret { get; set; }
        public decimal TradeAmount { get; set; }
        public int PollDelay { get; set; }
        public int BuyTimeAfterOpenThreshold { get; set; }
        public bool ShowAnalyzerDetails { get; set; } = true;

        public long TimeThatCanBuyUntil
        {
            get
            {
                return 1000 * 60 * (30 - BuyTimeAfterOpenThreshold);
            }
        }

        public decimal BtcAmount { get; set; } = 0.12m;
        public bool UseStairStepMethod { get; set; } = false;
    }
}
