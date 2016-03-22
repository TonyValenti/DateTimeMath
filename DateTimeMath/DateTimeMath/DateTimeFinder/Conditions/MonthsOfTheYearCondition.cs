using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateTimeMath.Search {
    public class MonthsOfTheYearCondition : DateTimeCondition {
        public MonthsOfYear MonthsOfTheYear { get; set; }


        public MonthsOfTheYearCondition() {
            this.MonthsOfTheYear = MonthsOfYear.Every;
        }

        public MonthsOfTheYearCondition(MonthsOfYear MonthsOfTheYear) {
            this.MonthsOfTheYear = MonthsOfTheYear;
        }

        public override bool IsTrue(DateTime Value) {
            return MonthsOfTheYear.Matches(Value);
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
