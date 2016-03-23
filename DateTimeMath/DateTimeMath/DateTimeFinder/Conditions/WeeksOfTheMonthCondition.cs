using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateTimeMath.Search {

    public class WeeksOfTheMonthCondition : DateTimeCondition {
        public WeeksOfMonth WeeksOfTheMonth { get; set; }

        public WeeksOfTheMonthCondition() {
            this.WeeksOfTheMonth = WeeksOfMonth.None;
        }

        public WeeksOfTheMonthCondition(WeeksOfMonth WeekOfMonth) {
            this.WeeksOfTheMonth = WeekOfMonth;
        }

        public override bool IsTrue(DateTime Value) {
            return WeeksOfTheMonth.Matches(Value);
        }

        public void Add(IEnumerable<WeeksOfMonth> WeeksOfTheMonth) {
            foreach (var item in WeeksOfTheMonth) {
                Add(item);
            }
        }

        public void Add(WeeksOfMonth WeeksOfTheMonth) {
            this.WeeksOfTheMonth |= WeeksOfTheMonth;
        }

        public void Remove(WeeksOfMonth WeeksOfTheMonth) {
            this.WeeksOfTheMonth &= ~WeeksOfTheMonth;
        }
    }

    public static partial class DateTimeFinderWithers {
        public static T On<T>(this T Composite, WeeksOfMonth WeeksOfTheMonth) where T : IContainsConditions {
            Composite.Conditions<WeeksOfTheMonthCondition>().Add(WeeksOfTheMonth);
            return Composite;
        }
    }

}
