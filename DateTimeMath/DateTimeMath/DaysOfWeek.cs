using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateTimeMath {
    [Flags]
    public enum DaysOfWeek {
        None = 0,
        Sunday = 1,
        Monday = 2,
        Tuesday = 4,
        Wednesday = 8,
        Thursday = 16,
        Friday = 32,
        Saturday = 64,

        Weekend = Sunday | Saturday,
        Weekday = Monday | Tuesday | Wednesday | Thursday | Friday,

        Every = Sunday | Monday | Tuesday | Wednesday | Thursday | Friday | Saturday
    }



    public static class DayOfWeekExtensions {
        private static readonly DaysOfWeek[] Order = new DaysOfWeek[] {
            DaysOfWeek.Sunday,
            DaysOfWeek.Monday,
            DaysOfWeek.Tuesday,
            DaysOfWeek.Wednesday,
            DaysOfWeek.Thursday,
            DaysOfWeek.Friday,
            DaysOfWeek.Saturday,
        };

        public static readonly DayOfWeek[] DayOfWeekOrder = new DayOfWeek[] {
            DayOfWeek.Sunday,
            DayOfWeek.Monday,
            DayOfWeek.Tuesday,
            DayOfWeek.Wednesday,
            DayOfWeek.Thursday,
            DayOfWeek.Friday,
            DayOfWeek.Saturday,
        };

        public static List<DaysOfWeek> GetValues(this DaysOfWeek Flags) {
            var ret = new List<DaysOfWeek>(Order.Length);
            foreach (var item in Order) {
                if (Flags.HasFlag(item)) {
                    ret.Add(item);
                }
            }

            return ret;
        }
        public static DayOfWeek ToDayOfWeek(this DaysOfWeek DaysOfTheWeek) {
            var V = DaysOfTheWeek.GetValues().First();
            var Index = Array.IndexOf(Order, V);

            return DayOfWeekOrder[Index];

        }

        public static DaysOfWeek ToDaysOfWeek(this DayOfWeek DayOfWeek) {
            var Index = Array.IndexOf(DayOfWeekOrder, DayOfWeek);
            return Order[Index];
        }
        


        public static bool Matches(this DaysOfWeek DaysOfTheWeek, DateTime Date) {
            var ret = false;
            for (int i = 0; i < Order.Length; i++) {
                if(DaysOfTheWeek.HasFlag(Order[i]) && (int)Date.DayOfWeek == i) {
                    ret = true;
                    break;
                }
            }

            return ret;
        }

      

    }
}
