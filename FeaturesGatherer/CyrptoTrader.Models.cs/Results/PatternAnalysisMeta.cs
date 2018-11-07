using CyrptoTrader.Models.Patterns;
using System;
using System.Collections.Generic;
using System.Text;

namespace CyrptoTrader.Models
{
    public class PatternAnalysisMeta
    {
        public PatternAnalysisMeta(Pattern pattern)
        {
            Pattern = pattern;
            FailData = new FailData();

        }
        public Pattern Pattern { get; set; }
        public int GivenTimeSpan
        {
            get
            {                
                return Pattern.TimeSpan;
            }
        }
        public decimal GivenTargetPercentage
        {
            get
            {                
                return Pattern.TargetPrice;
            }
        }
        public int TimesAppeared { get; set; }
        public int TimesExceededTargetPercentageWithInTimeSpan { get; set; }
        public int TimesExceededTargetWithInSecondCheck { get; set; }
        public int TimesExceededTargetWithInThirdCheck { get; set; }
        public int TimesExceededTargetWithInFourthCheck { get; set; }
        public FailData FailData { get; set; }

        public int InProgress { get; set; }
        public int TimesFellBelow { get; set; }
        public int TimesWinEndInPositive { get; set; }
        public decimal StairStepProfit { get; set; }
        public int TotalTimesExceeded
        {
            get
            {
                return TimesExceededTargetPercentageWithInTimeSpan + TimesExceededTargetWithInSecondCheck + TimesExceededTargetWithInThirdCheck + TimesExceededTargetWithInFourthCheck;
            }
        }
    }
}
