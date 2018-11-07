using System;
using System.Collections.Generic;
using System.Text;

namespace CyrptoTrader.Models
{
    public class FailData
    {
        public int Amount { get; set; }
        public decimal PercentageLoss { get; set; }
        public FailTimes TimePeriod { get; set; } = new FailTimes();
    }
    public class FailTimes
    {
        public int FourAmToTenAm { get; set; }
        public int TenAmToFourPm { get; set; }
        public int FourPmToTenPm { get; set; }
        public int TenPmToFourAm { get; set; }
    }
}
