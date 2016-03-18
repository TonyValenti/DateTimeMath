using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateTimeMath {
    public static class DateTimeFinderWithers {

        public static DateTimeFinder WithCondition(this DateTimeFinder DateFinder, TimeCondition Condition) {
            DateFinder.Conditions.Add(Condition);
            return DateFinder;
        }

        public static DateTimeFinder WithFilter(this DateTimeFinder DateFinder, TimeFilter Filter) {
            DateFinder.Filters.Add(Filter);
            return DateFinder;
        }

        public static DateTimeFinder Skip(this DateTimeFinder DateFinder, int ItemsToSkip) {
            DateFinder.Filters.Add(new SkipFilter(ItemsToSkip));
            return DateFinder;
        }

        public static DateTimeFinder AlternatingFirst(this DateTimeFinder DateFinder) {
            return DateFinder.AlternatingPattern(true, false);
        }

        public static DateTimeFinder AlternatingLast(this DateTimeFinder DateFinder) {
            return DateFinder.AlternatingPattern(false, true);
        }


        public static DateTimeFinder AlternatingPattern(this DateTimeFinder DateFinder, params bool[] Pattern) {
            DateFinder.Filters.Add(new AlternatingPatternFilter(Pattern));
            return DateFinder;
        }

        public static DateTimeFinder InEvenYears(this DateTimeFinder DateFinder) {
            DateFinder.Conditions.Add(new EvenYearsCondition());
            return DateFinder;
        }

        public static DateTimeFinder InOddYears(this DateTimeFinder DateFinder) {
            DateFinder.Conditions.Add(new OddYearsCondition());
            return DateFinder;
        }

        public static DateTimeFinder On(this DateTimeFinder DateFinder, DaysOfWeek DaysOfWeek) {
            DateFinder.Conditions.Add(new DaysOfWeekCondition(DaysOfWeek));
            return DateFinder;
        }

        public static DateTimeFinder On(this DateTimeFinder DateFinder, MonthsOfYear MonthsOfYear) {
            DateFinder.Conditions.Add(new MonthsOfYearCondition(MonthsOfYear));
            return DateFinder;
        }

        public static DateTimeFinder On(this DateTimeFinder DateFinder, WeeksOfMonth WeeksOfMonth) {
            DateFinder.Conditions.Add(new WeeksOfMonthCondition(WeeksOfMonth));
            return DateFinder;
        }

        public static DateTimeFinder On(this DateTimeFinder DateFinder, int DayOfTheMonth) {
            DateFinder.Conditions.Add(new DayOfMonthCondition(DayOfTheMonth));
            return DateFinder;
        }

        public static DateTimeFinder OnTheLastDayOfTheMonth(this DateTimeFinder DateFinder) {
            DateFinder.Conditions.Add(new LastDayOfMonthCondition());
            return DateFinder;
        }


       public static DateTimeFinder On(this DateTimeFinder DateFinder, params DateTime[] Dates) {
            DateFinder.Conditions.Add(new SpecificDateCondition(Dates));
            return DateFinder;
        }

        public static DateTimeFinder On(this DateTimeFinder DateFinder, IEnumerable<DateTime> Dates) {
            DateFinder.Conditions.Add(new SpecificDateCondition(Dates));
            return DateFinder;

        }

        public static DateTimeFinder At(this DateTimeFinder DateFinder, int Hour, int Minute) {
            DateFinder.Conditions.Add(new TimeTimeCondition(new DateTime(1, 1, 1, Hour, Minute, 0)));

            return DateFinder;
        }

        public static DateTimeFinder At(this DateTimeFinder DateFinder, int Hour) {
            return DateFinder.At(Hour, 0);
        }



    }
}
