using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateTimeMath {
    public static class DateTimeExtensions {

        public static DateTime WithTime(this DateTime DatePart, DateTime TimePart) {
            var ret = new DateTime(DatePart.Year, DatePart.Month, DatePart.Day, TimePart.Hour, TimePart.Minute, TimePart.Second, TimePart.Millisecond, DatePart.Kind);

            return ret;
        }

        public static DateTime ToMonth(this DateTime Date) {
            return new DateTime(Date.Year, Date.Month, 1);
        }

        public static DateTime ToYear(this DateTime Date) {
            return new DateTime(Date.Year, 1, 1);
        }

    }
}
