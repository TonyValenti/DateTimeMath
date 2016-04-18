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

        public override DateTime? NextTime(DateTime CurrentValue) {
            var ret = default(DateTime?);

            var NextTick = CurrentValue.AddTicks(1);
            if (IsTrue(NextTick)) {
                ret = NextTick;    
            } else if (WeeksOfTheMonth != WeeksOfMonth.None) {
                var PossibleValue = NextTick.Date;
                do {
                    PossibleValue = PossibleValue.AddDays(1);
                } while (!IsTrue(PossibleValue));

                ret = PossibleValue;
            }

            return ret;
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
