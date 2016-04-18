using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateTimeMath.Search {
    public class AtTimesCondition : DateTimeCondition {
        public List<DateTime> Times { get; private set; } = new List<DateTime>();

        public AtTimesCondition() {

        }


        public AtTimesCondition(params DateTime[] Times) {
            this.Times.AddRange(Times);
        }

        public AtTimesCondition(IEnumerable<DateTime> Times) {
            this.Times.AddRange(Times);
        }

        public override bool IsTrue(DateTime Value) {
            var Query = from x in Times
                        where x.TimeOfDay == Value.TimeOfDay
                        select x;

            return Query.Any();
        }

        public override DateTime? NextTime(DateTime CurrentValue) {

            var BaseDates = new[] { CurrentValue.Date, CurrentValue.Date.AddDays(1) };

            var PossibleDates = from BaseDate in BaseDates
                                from Time in Times
                                let NewDate = BaseDate.WithTime(Time)
                                where NewDate > CurrentValue
                                orderby NewDate ascending
                                select new DateTime?(NewDate);

            return PossibleDates.FirstOrDefault();
        }

    }

    public static partial class DateTimeFinderWithers {

        public static T AtTime<T>(this T DateFinder, string Time) where T : IContainsConditions {
            return AtTime(DateFinder, DateTime.Parse(Time));
        }

        public static T AtTime<T>(this T DateFinder, DateTime Time) where T : IContainsConditions {
            DateFinder.Conditions<AtTimesCondition>().Times.Add(new DateTime(1, 1, 1, Time.Hour, Time.Minute, Time.Second, Time.Millisecond));
            return DateFinder;
        }

        public static T AtTime<T>(this T DateFinder, int Hour, int Minute) where T : IContainsConditions {
            DateFinder.Conditions<AtTimesCondition>().Times.Add(new DateTime(1, 1, 1, Hour, Minute, 0));

            return DateFinder;
        }

        public static T AtTime<T>(this T DateFinder, int Hour) where T : IContainsConditions {
            DateFinder.Conditions<AtTimesCondition>().Times.Add(new DateTime(1, 1, 1, Hour, 0, 0));

            return DateFinder;
        }

        public static T AtMidnight<T>(this T DateFinder) where T : IContainsConditions {
            DateFinder.Conditions<AtTimesCondition>().Times.Add(new DateTime(1, 1, 1, 0, 0, 0));

            return DateFinder;
        }
    }
}
