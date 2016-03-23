using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DateTimeMath;

namespace DateTimeMath.Tests {
    public static class DateRanges {
        public static DateTimeRange Null {
            get {
                return null;
            }
        }

        public static DateTimeRange EmptyNow {
            get {
                var T = DateTime.Now;
                return new DateTimeRange(T, T);
            }
        }

        public static DateTimeRange EmptyReverse_1_3 {
            get {
                return new DateTimeRange(Day_1_3.EndDate, Day_1_3.StartDate);
            }
        }

        public static DateTimeRange EmptyReverse_3_5 {
            get {
                return new DateTimeRange(Day_3_5.EndDate, Day_3_5.StartDate);
            }
        }

        public static DateTimeRange Day_1_3 {
            get {
                return new DateTimeRange(
                "2016-01-01 8:00am",
                "2016-01-03 8:00am"
                );
            }
        }

        public static DateTimeRange Day_1_5 {
            get {
                return new DateTimeRange(
                "2016-01-01 8:00am",
                "2016-01-05 8:00am"
                );
            }
        }

        public static DateTimeRange Day_2_4 {
            get {
                return new DateTimeRange(
                "2016-01-02 8:00am",
                "2016-01-04 8:00am"
                );
            }
        }

        public static DateTimeRange Day_3_5 {
            get {
                return new DateTimeRange(
                "2016-01-03 8:00am",
                "2016-01-05 8:00am"
                );
            }
        }


    }
}
