using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateTimeMath.Search {

    public class DateTimeFinder : DateTimeFormula, IContainsConditions, IContainsAdjustments {
        public List<DateTimeAdjuster> Adjustments { get; private set; } = new List<DateTimeAdjuster>();
        public List<DateTimeCondition> Conditions { get; private set; } = new List<DateTimeCondition>();

        /*
        Adjustor        -   Before/After
        Condition       -   First/Monday/January
        Selector        -   First/Last/Skip/Pattern
        */


        private DateTimeFinder Clone() {
            var ret = new DateTimeFinder();
            ret.Adjustments.AddRange(Adjustments);
            ret.Conditions.AddRange(Conditions);
            return ret;
        }

        public override IEnumerable<DateTime?> Occurances(DateTime MinDate, DateTime MaxDate, DateTime StartDate) {
            IEnumerable<DateTime?> ret = null;

            if (Adjustments.Count > 0) {
                var Sub = this.Clone();
                var Adjuster = Sub.Adjustments[0];
                Sub.Adjustments.RemoveAt(0);
                ret = Adjuster.Adjust(Sub, MinDate, MaxDate, StartDate);

            } else {

                ret = AllDays(MinDate, MaxDate, StartDate);

                ret = (from x in ret where Conditions.TrueForAll(c => c.IsTrue(x.Value)) select x);

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
