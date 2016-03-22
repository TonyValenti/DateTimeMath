using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateTimeMath.Search {
    public class DaysOfTheMonthCondition : DateTimeCondition {
        public HashSet<int> DaysOfTheMonth { get; private set; } = new HashSet<int>();

        public bool Add(int DayOfTheMonth){
            return DaysOfTheMonth.Add(DayOfTheMonth);
        }

        public void Add(IEnumerable<int> DaysOfTheMonth) {
            foreach (var item in DaysOfTheMonth) {
                this.DaysOfTheMonth.Add(item);
            }
        }


        public DaysOfTheMonthCondition() {
        }

        public DaysOfTheMonthCondition(params int[] DaysOfTheMonth) {
            this.Add(DaysOfTheMonth);
        }

        public DaysOfTheMonthCondition(IEnumerable<int> DaysOfTheMonth) {
            this.Add(DaysOfTheMonth);
        }

        public override bool IsTrue(DateTime Value) {
            var Query =
                from x in DaysOfTheMonth
                where Value.Day == x
                select x;

            return Query.Any();
        }

    }

    public static partial class DateTimeFinderWithers {
        public static T On<T>(this T Composite, params int[] DaysOfTheMonth) where T : IContainsConditions {
            Composite.Conditions<DaysOfTheMonthCondition>().Add(DaysOfTheMonth);
            return Composite;
        }

        public static T On<T>(this T Composite, IEnumerable<int> DaysOfTheMonth) where T : IContainsConditions {
            Composite.Conditions<DaysOfTheMonthCondition>().Add(DaysOfTheMonth);
            return Composite;
        }


    }

}
