using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateTimeMath.Search {
    public class LastDayOfTheMonthCondition : DateTimeCondition {

        public override bool IsTrue(DateTime Value) {
            return Value.AddDays(1).Month != Value.Month;
        }

    }

    public static partial class DateTimeFinderWithers {

        public static T OnTheLastDayOfTheMonth<T>(this T Composite) where T : IContainsConditions {
            Composite.Conditions<LastDayOfTheMonthCondition>();
            return Composite;
        }

    }
}
