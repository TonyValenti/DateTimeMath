using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateTimeMath
{
    [Flags]
    public enum WeeksOfMonth
    {
        None = 0,
        First = 1,
        Second =2,
        Third = 4,
        Fourth = 8,
        Fifth = 16,
        Last = 32,
        Every = First | Second | Third | Fourth | Fifth
    }



    public static class WeeksOfMonthExtensions {
        public static readonly WeeksOfMonth[] Order = new WeeksOfMonth[] {
            WeeksOfMonth.First,
            WeeksOfMonth.Second,
            WeeksOfMonth.Third,
            WeeksOfMonth.Fourth,
            WeeksOfMonth.Fifth,
            WeeksOfMonth.Last,
        };

        public static List<WeeksOfMonth> GetValues(this WeeksOfMonth Flags) {
            var ret = new List<WeeksOfMonth>();
            foreach (var item in Order) {
                if (Flags.HasFlag(item)) {
                    ret.Add(item);
                }
            }

            return ret;
        }

        public static bool Matches(this WeeksOfMonth Frequency, DateTime Date)
        {
            var ret = false;

            var Number = Date.Day - 1;
            var ActualFrequency = Order[Number / 7];

            ret = Frequency.HasFlag(ActualFrequency);

            if (Frequency.HasFlag(WeeksOfMonth.Last)) {
                var NextWeek = Date.AddDays(7);
                if(NextWeek.Month != Date.Month) {
                    ret = true;
                }
            }

            return ret;
        }
    }
}
