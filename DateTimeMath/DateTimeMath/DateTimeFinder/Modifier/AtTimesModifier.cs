using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateTimeMath.Search {
    public class AtTimesModifier : DateTimeModifier {
        public List<DateTime> Times { get; private set; } = new List<DateTime>();

        public AtTimesModifier() {

        }


        public AtTimesModifier(params DateTime[] Times) {
            this.Times.AddRange(Times);
        }

        public AtTimesModifier(IEnumerable<DateTime> Times) {
            this.Times.AddRange(Times);
        }
        
        public override IEnumerable<DateTime?> Modify(IEnumerable<DateTime?> Source) {
            var Query =
                from x in Source
                from y in Times
                let NewValue = new DateTime(x.Value.Year, x.Value.Month, x.Value.Day, y.Hour, y.Minute, y.Second, y.Millisecond)
                select new DateTime?(NewValue);

            return Query;
        }
    }

    public static partial class DateTimeFinderWithers {
        public static T At<T>(this T DateFinder, int Hour, int Minute) where T : IContainsModifiers{
            DateFinder.Modifiers<AtTimesModifier>().Times.Add(new DateTime(1, 1, 1, Hour, Minute, 0));

            return DateFinder;
        }

        public static DateTimeFinder At(this DateTimeFinder DateFinder, int Hour) {
            return DateFinder.At(Hour, 0);
        }
    }
}
