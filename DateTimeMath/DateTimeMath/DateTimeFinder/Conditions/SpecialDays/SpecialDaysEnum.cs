using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateTimeMath.Search.SpecialDays {
    [Flags]
    public enum SpecialDaysEnum {
        None                    =   0,
        MartinLutherKingDay     =   1 << 0,
        SuperBowlSunday         =   1 << 1,
        ValentinesDay           =   1 << 2,
        PresidentsDay           =   1 << 3,
        SaintPatricksDay        =   1 << 4,
        Easter                  =   1 << 5,
        MothersDay              =   1 << 6,
        FathersDay              =   1 << 7,
        MemorialDay             =   1 << 8,
        LaborDay                =   1 << 9,
        Halloween               =   1 << 10,
        Thanksgiving            =   1 << 11,
        ChristmasEve            =   1 << 12,
        ChristmasDay            =   1 << 13,
        NewYearsEve             =   1 << 14,
        NewYearsDay             =   1 << 15,
    }



    public static class SpecialDaysExtensions {

        private static SpecialDaysEnum[] __Values;
        private static SpecialDaysEnum[] Values {
            get {
                if (__Values == null) {
                    __Values = (SpecialDaysEnum[]) Enum.GetValues(typeof(SpecialDaysEnum));
                }

                return __Values;
            }
        }

        public static List<SpecialDaysEnum> GetValues(this SpecialDaysEnum Flags) {

            var ret = new List<SpecialDaysEnum>(Values.Length);

            foreach (var value in Values) {
                if (Flags.HasFlag((SpecialDaysEnum)value)) {
                    ret.Add(value);
                }
            }

            
            
            return ret;
        }

        private static Dictionary<SpecialDaysEnum, DateTimeCondition> SpecialDaysDictionary = new Dictionary<SpecialDaysEnum, DateTimeCondition>() {
            {SpecialDaysEnum.ChristmasDay, new ChristmasDay() },
            {SpecialDaysEnum.ChristmasEve, new ChristmasEve() },
            {SpecialDaysEnum.Easter, new Easter() },
            {SpecialDaysEnum.FathersDay, new FathersDay() },
            {SpecialDaysEnum.Halloween, new Halloween() },
            {SpecialDaysEnum.LaborDay, new LaborDay() },
            {SpecialDaysEnum.MartinLutherKingDay, new MartinLutherKingDay() },
            {SpecialDaysEnum.MemorialDay, new MemorialDay() },
            {SpecialDaysEnum.MothersDay, new MothersDay() },
            {SpecialDaysEnum.NewYearsDay, new NewYearsDay() },
            {SpecialDaysEnum.NewYearsEve, new NewYearsEve() },
            {SpecialDaysEnum.PresidentsDay, new PresidentsDay() },
            {SpecialDaysEnum.SaintPatricksDay, new SaintPatricksDay() },
            {SpecialDaysEnum.SuperBowlSunday, new SuperBowlSunday() },
            {SpecialDaysEnum.Thanksgiving, new Thanksgiving() },
            {SpecialDaysEnum.ValentinesDay, new ValentinesDay() },

        };

        public static OrCondition SpecialDaysConditions(this SpecialDaysEnum SpecialDays) {
            var ret = new OrCondition();

            foreach (var Value in SpecialDays.GetValues()) {
                if (SpecialDaysDictionary.ContainsKey(Value)) {
                    ret.Conditions.Add(SpecialDaysDictionary[Value]);
                }
            }

            return ret;

        }

        public static bool Matches(this SpecialDaysEnum SpecialDays, DateTime Date) {
            var ret = false;

            foreach (var Value in SpecialDays.GetValues()) {
                if (SpecialDaysDictionary.ContainsKey(Value)) {
                    ret = SpecialDaysDictionary[Value].IsTrue(Date);
                    if (ret) {
                        break;
                    }
                }
            }

            return ret;
        }

      

    }
}
