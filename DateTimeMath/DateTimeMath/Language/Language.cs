using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateTimeMath {
    public static class Language {

        public static string Spell(int Number) {
            var st = "st";
            var nd = "nd";
            var rd = "rd";
            var th = "th";

            //                  0   1   2   3   4   5   6   7   8   9
            var index = new[] { th, st, nd, rd, th, th, th, th, th, th};

            var ret = Number.ToString();

            if(Number >= 0 && Number <= 9) {
                ret += index[Number];
            } else if (Number >= 10 && Number < 20) {
                ret += th;
            } else if(Number >= 20) {
                ret += index[Number % index.Length];
            }

            return ret;
        }

        public static string Spell(DateTime Date) {
            var ret = "";
            //ret = String.Format("the {0} day of ", Language.Spell(Day)



            return ret;
        }


        public static string ListAnd<T>(List<T> Items) {
            return List(Items, "and");
        }

        public static string ListOr<T>(List<T> Items) {
            return List(Items, "or");
        }

        public static string List<T>(List<T> Items, string Conjunction) {
            var ret = "";
            var Separator = "";
            for (int i = 0; i < Items.Count; i++) {

                if (i == 0) {
                    Separator = "";
                } else if (i == Items.Count - 1) {
                    if (i == 1) {
                        Separator = ", " + Conjunction + " ";
                    } else {
                        Separator = " " + Conjunction + " ";
                    }
                } else {
                    Separator = ", ";
                }

                ret += Separator + Items[i].ToString();
            }

            return ret;
        }

        public static string AtTime(DateTime Time) {
            return AtTime(Time.Hour, Time.Minute);
        }

        public static string AtTime(int Hour, int Minute) {
            var ret = "";
            if (!(Hour == 0 && Minute == 0)) {
                var Mode = (Hour < 12 ? "AM" : "PM");

                if (Hour > 12) {
                    Hour -= 12;
                }

                ret = String.Format(" at {0:##}:{1:00} {2}", Hour, Minute, Mode);
            }

            return ret;
        }

    }
}
