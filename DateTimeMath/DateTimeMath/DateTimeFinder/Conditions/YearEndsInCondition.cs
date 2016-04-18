using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateTimeMath.Search {
    public class YearEndsInCondition : DateTimeCondition {
        public HashSet<int> Endings { get; private set; } = new HashSet<int>();

        public void Add(int Digits) {
            Endings.Add(Digits);
        }

        public void Add(IEnumerable<int> Digits) {
            foreach (var item in Digits) {
                this.Add(item);
            }
        }

        public void Remove(int Digits) {
            Endings.Remove(Digits);
        }

        public void Remove(IEnumerable<int> Digits) {
            foreach (var item in Digits) {
                this.Remove(item);
            }
        }

        public override bool IsTrue(DateTime Value) {
            var ret = true;

            if (Endings.Count > 0) {
                var Query =
                    from x in Endings
                    where Value.Year.ToString().EndsWith(x.ToString())
                    select x
                    ;

                ret = Query.Any();
            }

            return ret;
        }

        public override DateTime? NextTime(DateTime CurrentValue) {
            var ret = default(DateTime?);

            var NextTick = CurrentValue.AddTicks(1);

            if (IsTrue(NextTick)) {
                ret = NextTick;
            } else {
                var NextYear = NextTick;
                do {
                    NextYear = new DateTime(NextYear.Year + 1, 1, 1);
                } while (!IsTrue(NextYear));
                ret = NextYear;
            }

            return ret;
        }

    }

    public static partial class DateTimeFinderWithers {
        public static T YearEndsIn<T>(this T Composite, params int[] LastDigitsOfTheYear) where T : IContainsConditions {
            var Condition = Composite.Conditions<YearEndsInCondition>();
            Condition.Add(LastDigitsOfTheYear);

            return Composite;
        }

        public static T YearEndsIn<T>(this T Composite, IEnumerable<int> LastDigitsOfTheYear) where T : IContainsConditions {
            var Condition = Composite.Conditions<YearEndsInCondition>();
            Condition.Add(LastDigitsOfTheYear);

            return Composite;
        }

    }

}
