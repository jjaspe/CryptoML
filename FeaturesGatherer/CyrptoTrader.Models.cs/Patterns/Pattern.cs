using System;
using System.Collections.Generic;
using System.Text;

namespace CyrptoTrader.Models.Patterns
{
    public abstract class Pattern
    {
        public decimal TargetPrice { get; set; }
        public int Priority { get; set; }
        public bool Enabled { get; set; }
        public int TimeSpan { get; set; }
        public decimal ClampAmount { get; set; }
        public bool RestrictByMA { get; set; } = false;
    }
}
