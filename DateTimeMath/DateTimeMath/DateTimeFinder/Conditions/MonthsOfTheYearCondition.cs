using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateTimeMath.Search {
    public class MonthsOfTheYearCondition : DateTimeCondition {
        public MonthsOfYear MonthsOfTheYear { get; set; }


        public MonthsOfTheYearCondition() {
            this.MonthsOfTheYear = MonthsOfYear.None;
        }

        public MonthsOfTheYearCondition(MonthsOfYear MonthsOfTheYear) {
            this.MonthsOfTheYear = MonthsOfTheYear;
        }

        public override bool IsTrue(DateTime Value) {
            return MonthsOfTheYear.Matches(Value);
        }

        public override DateTime? NextTime(DateTime CurrentValue) {
            var ret = default(DateTime?);

            var NextTick = CurrentValue.AddTicks(1);
            if (IsTrue(NextTick)) {
                ret = NextTick;
            } else if (MonthsOfTheYear != MonthsOfYear.None) {
                var PossibleDate = NextTick.ToMonth();
                do {
                    PossibleDate = PossibleDate.AddMonths(1);
                } while (!IsTrue(PossibleDate));

                ret = PossibleDate;
            }

            return ret;
        }


        public void Add(MonthsOfYear MonthsOfTheYear) {
            this.MonthsOfTheYear |= MonthsOfTheYear;
        }

        public void Remove(MonthsOfYear MonthsOfTheYear) {
            this.MonthsOfTheYear &= ~MonthsOfTheYear;
        }

    }

    public static partial class DateTimeFinderWithers {
        public static T On<T>(this T Composite, MonthsOfYear MonthsOfTheYear) where T : IContainsConditions {
            Composite.Conditions<MonthsOfTheYearCondition>().Add(MonthsOfTheYear);
            return Composite;
        }
    }

}
