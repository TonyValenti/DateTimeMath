using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateTimeMath {
    public abstract class TimeFilter {
        public abstract IEnumerable<DateTime?> Filter(IEnumerable<DateTime?> Source);
    }
    
    public class SkipFilter : TimeFilter {

        public int ItemsToSkip { get; set; } = 0;

        public SkipFilter() {
            
        }

        public SkipFilter(int ItemsToSkip) {
            this.ItemsToSkip = ItemsToSkip;
        }

        public override IEnumerable<DateTime?> Filter(IEnumerable<DateTime?> Source) {
            return Source.Skip(ItemsToSkip);
        }
    }

    public class AlternatingPatternFilter : TimeFilter {
        public List<bool> Pattern { get; private set; } = new List<bool>();

        public AlternatingPatternFilter() {

        }

        public AlternatingPatternFilter(params bool[] Pattern) {
            this.Pattern.AddRange(Pattern);
        }

        public AlternatingPatternFilter(IEnumerable<bool> Pattern) {
            this.Pattern.AddRange(Pattern);
        }

        public override IEnumerable<DateTime?> Filter(IEnumerable<DateTime?> Source) {
            var Index = 0;
            var IE = Source.GetEnumerator();
            while (IE.MoveNext()) {
                var ShouldRet = true;

                if (Pattern.Count > 0) {
                    ShouldRet = Pattern[Index];
                    Index = (Index + 1) % Pattern.Count;
                }

                if (ShouldRet) {
                    yield return IE.Current;
                }
            }
        }
    }

    public class LastFilter : TimeFilter {
        public override IEnumerable<DateTime?> Filter(IEnumerable<DateTime?> Source) {
            yield return Source.LastOrDefault();
        }
    }

    public class FirstFilter : TimeFilter {
        public override IEnumerable<DateTime?> Filter(IEnumerable<DateTime?> Source) {
            yield return Source.FirstOrDefault();
        }
    }

    public class BlackoutFilter : TimeFilter {
        public List<DateTimeRangeFormula> BlackoutDates { get; private set; } = new List<DateTimeRangeFormula>();

        public override IEnumerable<DateTime?> Filter(IEnumerable<DateTime?> Source) {
            var Query =
                from x in Source
                where !Matches(x)
                select x;

            return Query;
        }

        private bool Matches(DateTime? Value) {
            var Query = from x in BlackoutDates
                        from occurance in x.Occurances(DateTime.MinValue, DateTime.MaxValue)
                        where occurance.Intersects(Value)
                        select x;

            return Query.Any();
        }
    }

}
