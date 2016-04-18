using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateTimeMath.Search.SpecialDays {
    public class ValentinesDay : DateTimeCondition {

        private static AndCondition SubCondition = 
            new AndCondition()
            .On(MonthsOfYear.February)
            .On(14)
            ;

        public override bool IsTrue(DateTime Value) {
            return SubCondition.IsTrue(Value);
        }

        public override DateTime? NextTime(DateTime CurrentValue) {
            return SubCondition.NextTime(CurrentValue);
        }
    }
}
