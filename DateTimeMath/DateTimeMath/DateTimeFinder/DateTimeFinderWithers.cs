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



    }
}
