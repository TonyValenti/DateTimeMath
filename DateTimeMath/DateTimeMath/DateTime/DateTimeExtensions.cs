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

    }
}
