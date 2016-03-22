using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateTimeMath.Search.SpecialDays {
    [Flags]
    public enum SpecialDaysEnum {
        None = 0,
        MartinLutherKingDay,
        SuperBowlSunday,
        ValentinesDay,
        PresidentsDay,
        SaintPatricksDay,
        Easter,
        MothersDay,
        FathersDay,
        MemorialDay,
        LaborDay,
        Halloween,
        Thanksgiving,
        ChristmasEve,
        ChristmasDay,
        NewYearsEve,
        NewYearsDay,

    }



    public static class SpecialDaysExtensions {

        public static List<SpecialDaysEnum> GetValues(this SpecialDaysEnum Flags) {
            var Values = Enum.GetValues(typeof(SpecialDaysEnum));

            var ret = new List<SpecialDaysEnum>(Values.Length);

            foreach (SpecialDaysEnum value in Values) {
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
