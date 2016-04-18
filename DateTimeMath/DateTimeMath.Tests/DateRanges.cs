using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DateTimeMath;

namespace DateTimeMath.Tests {
    public static class DateRanges {
        public static DateSpan Null {
            get {
                return null;
            }
        }

        public static DateSpan EmptyNow {
            get {
                var T = DateTime.Now;
                return new DateSpan(T, T);
            }
        }

        public static DateSpan EmptyReverse_1_3 {
            get {
                return new DateSpan(Day_1_3.EndDate, Day_1_3.StartDate);
            }
        }

        public static DateSpan EmptyReverse_3_5 {
            get {
                return new DateSpan(Day_3_5.EndDate, Day_3_5.StartDate);
            }
        }

        public static DateSpan Day_1_3 {
            get {
                return new DateSpan(
                "2016-01-01 8:00am",
                "2016-01-03 8:00am"
                );
            }
        }

        public static DateSpan Day_1_5 {
            get {
                return new DateSpan(
                "2016-01-01 8:00am",
                "2016-01-05 8:00am"
                );
            }
        }

        public static DateSpan Day_2_4 {
            get {
                return new DateSpan(
                "2016-01-02 8:00am",
                "2016-01-04 8:00am"
                );
            }
        }

        public static DateSpan Day_3_5 {
            get {
                return new DateSpan(
                "2016-01-03 8:00am",
                "2016-01-05 8:00am"
                );
            }
        }


    }
}
