using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateTimeMath.Search {
    public static partial class DateTimeFinderWithers {

     

        public static DateTimeFinder WithAdjustment(this DateTimeFinder DateFinder, DateTimeAdjuster Adjustment) {
            DateFinder.Adjustments.Add(Adjustment);
            return DateFinder;
        }

        public static DateTimeFinder Skip(this DateTimeFinder DateFinder, int ItemsToSkip) {
            DateFinder.Selectors.Add(new SkipSelector(ItemsToSkip));
            return DateFinder;
        }

        public static DateTimeFinder AlternatingFirst(this DateTimeFinder DateFinder) {
            return DateFinder.AlternatingPattern(true, false);
        }

        public static DateTimeFinder AlternatingLast(this DateTimeFinder DateFinder) {
            return DateFinder.AlternatingPattern(false, true);
        }


        public static DateTimeFinder AlternatingPattern(this DateTimeFinder DateFinder, params bool[] Pattern) {
            DateFinder.Selectors.Add(new AlternatingPatternSelector(Pattern));
            return DateFinder;
        }


    }
}
