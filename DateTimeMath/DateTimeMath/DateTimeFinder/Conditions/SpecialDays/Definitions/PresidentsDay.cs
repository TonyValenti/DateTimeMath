using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateTimeMath.Search.SpecialDays {
    public class PresidentsDay : DateTimeCondition {

        private static AndCondition SubCondition = 
            new AndCondition()
            .On(WeeksOfMonth.Third)
            .On(DaysOfWeek.Monday)
            .On(MonthsOfYear.February)
            ;

        public override bool IsTrue(DateTime Value) {
            return SubCondition.IsTrue(Value);
        }
    }
}
