using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateTimeMath.Search {
    public class SpecificDateCondition : DateTimeCondition {
        public HashSet<DateTime> Dates { get; private set; } = new HashSet<DateTime>();

        public SpecificDateCondition() {

        }

        public SpecificDateCondition(params DateTime[] Dates) {
            this.Add(Dates);
        }

        public SpecificDateCondition(IEnumerable<DateTime> Dates) {
            this.Add(Dates);
        }

        public void Add(IEnumerable<DateTime> Values) {
            foreach (var item in Values) {
                Add(item);
            }
        }

        public void Add(IEnumerable<DateTime?> Values) {
            foreach (var item in Values) {
                Add(item);
            }
        }

        public void Add(DateTime Value) {
            this.Dates.Add(Value);
        }

        public void Add(DateTime? Value) {
            if (Value.HasValue) {
                Add(Value.Value);
            }
        }

        public override bool IsTrue(DateTime Value) {
            var Query =
                from x in Dates
                where x.Date == Value.Date
                select x;

            return Query.Any();
        }

        public override DateTime? NextTime(DateTime CurrentValue) {

            var Query =
                from x in Dates
                where x.Date > CurrentValue
                orderby x ascending
                select new DateTime?(x);

            var ret = Query.FirstOrDefault();

            return ret;
        }

    }

    public static partial class DateTimeFinderWithers {

        public static T On<T>(this T Composite, params DateTime[] Dates) where T : IContainsConditions {
            Composite.Conditions<SpecificDateCondition>().Add(Dates);
            return Composite;
        }

        public static T On<T>(this T Composite, IEnumerable<DateTime> Dates) where T : IContainsConditions {
            Composite.Conditions<SpecificDateCondition>().Add(Dates);
            return Composite;
        }
    }

}
