using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateTimeMath.Search.SpecialDays {
    public class LaborDay : DateTimeCondition {

        private static AndCondition SubCondition = 
            new AndCondition()
            .On(WeeksOfMonth.First)
            .On(DaysOfWeek.Monday)
            .On(MonthsOfYear.September)
            ;

        public override bool IsTrue(DateTime Value) {
            return SubCondition.IsTrue(Value);
        }

        public override DateTime? NextTime(DateTime CurrentValue) {
            return SubCondition.NextTime(CurrentValue);
        }
    }
}
