using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateTimeMath.Search
{

    public class DateTimeFinder : DateTimeFormula, IContainsConditions, IContainsModifiers, IContainsAdjustments, IContainsSelectors
    {
        public List<DateTimeCondition> Conditions { get; private set; } = new List<DateTimeCondition>();
        public List<DateTimeModifier> Modifiers { get; private set; } = new List<DateTimeModifier>();
        public List<DateTimeAdjuster> Adjustments { get; private set; } = new List<DateTimeAdjuster>();
        public List<DateTimeSelector> Selectors { get; private set; } = new List<DateTimeSelector>();

        /*
        Condition       -   First/Monday/January
        Modifier        -   At
        Adjustor        -   Before/After
        Selector        -   First/Last/Skip/Pattern
        */
        
        public override IEnumerable<DateTime?> Occurances(DateTime MinDate, DateTime MaxDate, DateTime StartDate) {
            var ret = AllDays(MinDate, MaxDate, StartDate);

            ret = (from x in ret where Conditions.TrueForAll(c => c.IsTrue(x.Value)) select x);

            

            foreach (var item in Adjustments) {
                ret = item.Adjust(ret);
            }

            return ret;
        }

        protected IEnumerable<DateTime?> AllDays(DateTime MinDate, DateTime MaxDate, DateTime StartDate) {
            var Modifier = (MinDate < MaxDate ? 1 : -1);
            var LowDate = (MinDate < MaxDate ? MinDate : MaxDate);
            var HighDate = (MaxDate > MinDate ? MaxDate : MinDate);

            var BaseDate = StartDate;

            while (BaseDate >= LowDate && BaseDate <= HighDate) {
                yield return BaseDate;
                BaseDate = BaseDate.AddDays(Modifier);
            }
            

        }


    }
}
