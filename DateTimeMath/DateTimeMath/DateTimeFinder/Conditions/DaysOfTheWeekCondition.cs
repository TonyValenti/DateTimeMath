using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateTimeMath.Search {

    public class DaysOfTheWeekCondition : DateTimeCondition {
        public DaysOfWeek DaysOfTheWeek { get; set; }

        public DaysOfTheWeekCondition() {
            this.DaysOfTheWeek = DaysOfWeek.None;
        }

        public DaysOfTheWeekCondition(DaysOfWeek DaysOfTheWeek) {
            this.DaysOfTheWeek = DaysOfTheWeek;
        }

        public override bool IsTrue(DateTime Value) {
            return DaysOfTheWeek.Matches(Value);
        }

        public override DateTime? NextTime(DateTime CurrentValue) {
            var ret = default(DateTime?);

            var NextTick = CurrentValue.AddTicks(1);
            if (IsTrue(NextTick)) {
                ret = NextTick;
            } else if(DaysOfTheWeek != DaysOfWeek.None) {
                var PossibleNextTime = CurrentValue.Date;
                do {
                    PossibleNextTime = PossibleNextTime.AddDays(1);
                } while (!IsTrue(PossibleNextTime));

                ret = PossibleNextTime;
            }

            return ret;
        }

        public void Add(DaysOfWeek DaysOfTheWeek) {
            this.DaysOfTheWeek |= DaysOfTheWeek;
        }

        public void Add(IEnumerable<DaysOfWeek> DaysOfTheWeek) {
            foreach (var item in DaysOfTheWeek) {
                this.Add(item);
            }
        }

        public void Remove(DaysOfWeek DaysOfTheWeek) {
            this.DaysOfTheWeek &= ~DaysOfTheWeek;
        }

        public void Remove(IEnumerable<DaysOfWeek> DaysOfTheWeek) {
            foreach (var item in DaysOfTheWeek) {
                this.Remove(item);
            }
        }

    }

    public static partial class DateTimeFinderWithers {

        public static T On<T>(this T Composite, DaysOfWeek DaysOfTheWeek) where T : IContainsConditions {
            Composite.Conditions<DaysOfTheWeekCondition>().Add(DaysOfTheWeek);
            return Composite;
        }

        public static T On<T>(this T Composite, DayOfWeek Day) where T : IContainsConditions {
            Composite.Conditions<DaysOfTheWeekCondition>().Add(Day.ToDaysOfWeek());
            return Composite;
        }
    }

}
