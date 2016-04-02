using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateTimeMath.Search {
  

    public class BlackoutSelector : DateTimeSelector {
        public List<DateTimeRangeFormula> BlackoutDates { get; private set; } = new List<DateTimeRangeFormula>();

        public override IEnumerable<DateTime?> Select(IEnumerable<DateTime?> Source) {
            var Query =
                from x in Source
                where !Matches(x)
                select x;

            return Query;
        }

        private bool Matches(DateTime? Value) {
            var Query = from x in BlackoutDates
                        from occurance in x.Occurances(DateTime.MinValue, DateTime.MaxValue)
                        where occurance.During(Value)
                        select x;

            return Query.Any();
        }
    }


}
