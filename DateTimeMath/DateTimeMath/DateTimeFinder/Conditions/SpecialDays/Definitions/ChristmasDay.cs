using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateTimeMath.Search.SpecialDays {
    public class ChristmasDay : DateTimeCondition {

        private static AndCondition SubCondition = 
            new AndCondition()
            .On(MonthsOfYear.December)
            .On(25)
            ;

        public override bool IsTrue(DateTime Value) {
            return SubCondition.IsTrue(Value);
        }
    }
}
