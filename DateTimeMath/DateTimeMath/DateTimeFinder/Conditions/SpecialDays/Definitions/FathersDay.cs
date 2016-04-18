using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateTimeMath.Search.SpecialDays {
    public class FathersDay : DateTimeCondition {

        private static AndCondition SubCondition = 
            new AndCondition()
                .On(WeeksOfMonth.Third)
                .On(DaysOfWeek.Sunday)
                .On(MonthsOfYear.June)
                ;

        public override bool IsTrue(DateTime Value) {
            return SubCondition.IsTrue(Value);
        }

        public override DateTime? NextTime(DateTime CurrentValue) {
            return SubCondition.NextTime(CurrentValue);
        }
    }
}
