using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateTimeMath {
    [Flags]
    public enum MonthsOfYear {
        None        = 0,
        January     = 1 << 0,
        February    = 1 << 1,
        March       = 1 << 2,
        April       = 1 << 3,
        May         = 1 << 4,
        June        = 1 << 5,
        July        = 1 << 6,
        August      = 1 << 7,
        September   = 1 << 8,
        October     = 1 << 9,
        November    = 1 << 10,
        December    = 1 << 11,

        Every = January | February | March | April | May | June | July | August | September | October | November | December
    }

    public static class MonthsOfTheYearExtensions {
        private static readonly MonthsOfYear[] Order = new MonthsOfYear[] {
                MonthsOfYear.January, MonthsOfYear.February, MonthsOfYear.March, MonthsOfYear.April
                , MonthsOfYear.May, MonthsOfYear.June, MonthsOfYear.July, MonthsOfYear.August
                , MonthsOfYear.September, MonthsOfYear.October, MonthsOfYear.November, MonthsOfYear.December
            };

        public static List<MonthsOfYear> GetValues(this MonthsOfYear Flags) {
            var ret = new List<MonthsOfYear>(Order.Length);
            foreach (var item in Order) {
                if (Flags.HasFlag(item)) {
                    ret.Add(item);
                }
            }

            return ret;
        }

        public static bool Matches(this MonthsOfYear Flags, DateTime DateTime) {
            return (Flags & GetMonth(DateTime.Month)) != MonthsOfYear.None;
        }

        public static MonthsOfYear GetMonth(int Month) {
            return Order[Month - 1];
        }


        public static int GetMonth(this MonthsOfYear Month) {
            var ret = 0;

            var FlagValues = GetValues(Month);
            if (FlagValues.Count > 0) {
                for (int i = 0; i < Order.Length; ++i) {
                    if (Order[i] == FlagValues[0]) {
                        ret = i + 1;
                        break;
                    }
                }
            }

            return ret;
        }

       

    }
}