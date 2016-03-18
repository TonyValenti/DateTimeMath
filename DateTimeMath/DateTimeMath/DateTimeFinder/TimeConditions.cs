using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateTimeMath {
    public abstract class TimeCondition {

        public abstract bool IsTrue(DateTime Value);

        public TimeCondition() {
            
        }

    }

    public class AndTimeCondition : TimeCondition {
        public List<TimeCondition> Conditions { get; private set; } = new List<TimeCondition>();

        public AndTimeCondition() {

        }
        public AndTimeCondition(IEnumerable<TimeCondition> Conditions) {
            this.Conditions.AddRange(Conditions);
        }
        public AndTimeCondition(params TimeCondition[] Conditions) {
            this.Conditions.AddRange(Conditions);
        }

        public override bool IsTrue(DateTime Value) {
            var ret = true;
            if(Conditions.Count > 0) {
                ret = Conditions.TrueForAll(x => x.IsTrue(Value));
            }
            return ret;
        }
    }

    public class OrTimeCondition : TimeCondition {
        public List<TimeCondition> Conditions { get; private set; } = new List<TimeCondition>();

        public OrTimeCondition() {

        }
        public OrTimeCondition(IEnumerable<TimeCondition> Conditions) {
            this.Conditions.AddRange(Conditions);
        }
        public OrTimeCondition(params TimeCondition[] Conditions) {
            this.Conditions.AddRange(Conditions);
        }

        public override bool IsTrue(DateTime Value) {
            var ret = true;
            if (Conditions.Count > 0) {
                ret = Conditions.Any(x => x.IsTrue(Value));
            }
            return ret;
        }
    }



    [Flags]
    public enum Parity {
        None        =   0,
        Odd         =   1,
        Even        =   2,
        Every       =   Odd | Even,
    }

    public class YearsParityCondition : TimeCondition {
        public Parity Parity { get; set; }

        public YearsParityCondition() : this(Parity.Every) {

        }

        public YearsParityCondition(Parity Parity) {
            this.Parity = Parity;
        }

        public override bool IsTrue(DateTime Value) {
            var ret = (Parity.HasFlag(Parity.Even) && Value.Year % 2 == 0)
                    ||
                    (Parity.HasFlag(Parity.Odd) && Value.Year % 2 == 1);

            return ret;
        }

    }

    public class EvenYearsCondition : TimeCondition {

        public override bool IsTrue(DateTime Value) {
            return Value.Year % 2 == 0;
        }
    }

    public class OddYearsCondition : TimeCondition {

        public override bool IsTrue(DateTime Value) {
            return Value.Year % 2 == 1;
        }

    }

    public class DaysOfWeekCondition : TimeCondition {
        public DaysOfWeek DaysOfWeek { get; set; }

        public DaysOfWeekCondition() {
            this.DaysOfWeek = DaysOfWeek.Every;
        }

        public DaysOfWeekCondition(DaysOfWeek DaysOfWeek) {
            this.DaysOfWeek = DaysOfWeek;
        }

        public override bool IsTrue(DateTime Value) {
            return DaysOfWeek.Matches(Value);
        }

    }

    public class MonthsOfYearCondition : TimeCondition {
        public MonthsOfYear MonthsOfYear { get; set; }


        public MonthsOfYearCondition() {
            this.MonthsOfYear = MonthsOfYear.Every;
        }

        public MonthsOfYearCondition(MonthsOfYear MonthsOfYear) {
            this.MonthsOfYear = MonthsOfYear;
        }

        public override bool IsTrue(DateTime Value) {
            return MonthsOfYear.Matches(Value);
        }

    }

    public class WeeksOfMonthCondition : TimeCondition {
        public WeeksOfMonth WeekOfMonth { get; set; }

        public WeeksOfMonthCondition() {
            this.WeekOfMonth = WeeksOfMonth.Every;
        }

        public WeeksOfMonthCondition(WeeksOfMonth WeekOfMonth) {
            this.WeekOfMonth = WeekOfMonth;
        }

        public override bool IsTrue(DateTime Value) {
            return WeekOfMonth.Matches(Value);
        }
    }

    public class DayOfMonthCondition : TimeCondition {
        public int DayOfMonth { get; set; }

        public DayOfMonthCondition() {
            this.DayOfMonth = 1;
        }

        public DayOfMonthCondition(int DayOfMonth) {
            this.DayOfMonth = DayOfMonth;
        }

        public override bool IsTrue(DateTime Value) {
            return Value.Day == DayOfMonth;
        }
    }

    public class LastDayOfMonthCondition : TimeCondition {

        public override bool IsTrue(DateTime Value) {
            return Value.AddDays(1).Month != Value.Month;
        }

    }

    public class EasterTimeCondition : TimeCondition {

        public override bool IsTrue(DateTime Value) {
            return Value.Date == EasterSunday(Value.Year);
        }

        private DateTime EasterSunday(int year) {
            int day = 0;
            int month = 0;

            int g = year % 19;
            int c = year / 100;
            int h = (c - (int)(c / 4) - (int)((8 * c + 13) / 25) + 19 * g + 15) % 30;
            int i = h - (int)(h / 28) * (1 - (int)(h / 28) * (int)(29 / (h + 1)) * (int)((21 - g) / 11));

            day = i - ((year + (int)(year / 4) + i + 2 - c + (int)(c / 4)) % 7) + 28;
            month = 3;

            if (day > 31) {
                month++;
                day -= 31;
            }

            return new DateTime(year, month, day);
        }
    }

    public class SpecificDateCondition : TimeCondition {
        public List<DateTime> Dates { get; private set; } = new List<DateTime>();

        public SpecificDateCondition() {

        }

        public SpecificDateCondition(params DateTime[] Dates) {
            this.Dates.AddRange(Dates);
        }

        public SpecificDateCondition(IEnumerable<DateTime> Dates) {
            this.Dates.AddRange(Dates);
        }

        public override bool IsTrue(DateTime Value) {
            var Query =
                from x in Dates
                where x.Date == Value.Date
                select x;

            return Query.Any();
        }

    }
  
    public class TimeTimeCondition : TimeCondition {
        public List<DateTime> Times { get; private set; } = new List<DateTime>();

        public TimeTimeCondition() {

        }

  
        public TimeTimeCondition(params DateTime[] Times) {
            this.Times.AddRange(Times);
        }

        public TimeTimeCondition(IEnumerable<DateTime> Times) {
            this.Times.AddRange(Times);
        }


        protected override IEnumerable<DateTime?> ApplyInternal(IEnumerable<DateTime?> Source) {
            var Query =
                from x in Source
                from y in Times
                let NewValue = new DateTime(x.Value.Year, x.Value.Month, x.Value.Day, y.Hour, y.Minute, y.Second, y.Millisecond)
                select new DateTime?(NewValue);

            return Query;
        }
    }

}
