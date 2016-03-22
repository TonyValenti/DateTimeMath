using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateTimeMath.Search.SpecialDays {
    public class MemorialDay : DateTimeCondition {

        private static AndCondition SubCondition = 
            new AndCondition()
            .On(WeeksOfMonth.Last)
            .On(DaysOfWeek.Monday)
            .On(MonthsOfYear.May)
            ;

        public override bool IsTrue(DateTime Value) {
            return SubCondition.IsTrue(Value);
        }
    }
}
