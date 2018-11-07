using CyrptoTrader.Models.Patterns;
using System;
using System.Collections.Generic;
using System.Text;

namespace CyrptoTrader.Data
{
    public class PatternData
    {
        public PatternData(Pattern pattern)
        {
            if(pattern != null)
            {
                Name = pattern.ToString();
                TargetPrice = pattern.TargetPrice;
                Priority = pattern.Priority;
                Enabled = pattern.Enabled;
                TimeSpan = pattern.TimeSpan;
            }
        }
        public string Name { get; set; }
        public decimal TargetPrice { get; set; }
        public int Priority { get; set; }
        public bool Enabled { get; set; }
        public int TimeSpan { get; set; }
    }
}
