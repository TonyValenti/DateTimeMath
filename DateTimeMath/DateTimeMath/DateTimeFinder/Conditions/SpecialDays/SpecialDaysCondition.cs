using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateTimeMath.Search.SpecialDays {
    public class SpecialDaysCondition : DateTimeCondition {
        private SpecialDaysEnum __SpecialDays;
        public SpecialDaysEnum SpecialDays {
            get {
                return __SpecialDays;
            }
            set {
                __SpecialDays = value;
                this.SubCondition = this.SpecialDays.SpecialDaysConditions();
            }
        }

        private OrCondition SubCondition = new OrCondition();


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

        public override DateTime? NextTime(DateTime CurrentValue) {
            return SubCondition.NextTime(CurrentValue);
        }

    }

    public static partial class DateTimeFinderWithers {
        public static DateTimeFinder On(this DateTimeFinder DateFinder, SpecialDaysEnum SpecialDays) {
            DateFinder.Conditions<SpecialDaysCondition>().Add(SpecialDays);
            return DateFinder;
        }

    }

}