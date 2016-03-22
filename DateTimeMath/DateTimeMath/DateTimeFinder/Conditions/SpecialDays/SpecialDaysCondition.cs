using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateTimeMath.Search.SpecialDays {
    public class SpecialDaysCondition : DateTimeCondition {
        public SpecialDaysEnum SpecialDays { get; set; }

        public SpecialDaysCondition() {

        }

        public SpecialDaysCondition(SpecialDaysEnum SpecialDays) {
            this.SpecialDays = SpecialDays;
        }

        public void Add(IEnumerable<SpecialDaysEnum> SpecialDays) {
            foreach (var item in SpecialDays) {
                Add(item);
            }
        }

        public void Add(SpecialDaysEnum SpecialDays) {
            this.SpecialDays |= SpecialDays;
        }

        public void Remove(IEnumerable<SpecialDaysEnum> SpecialDays) {
            foreach (var item in SpecialDays) {
                Remove(item);
            }
        }

        public void Remove(SpecialDaysEnum SpecialDays) {
            this.SpecialDays &= ~SpecialDays;
        }

        public override bool IsTrue(DateTime Value) {
            return SpecialDays.Matches(Value);
        }

    }

    public static partial class DateTimeFinderWithers {
        public static DateTimeFinder On(this DateTimeFinder DateFinder, SpecialDaysEnum SpecialDays) {
            DateFinder.Conditions<SpecialDaysCondition>().Add(SpecialDays);
            return DateFinder;
        }

    }

}