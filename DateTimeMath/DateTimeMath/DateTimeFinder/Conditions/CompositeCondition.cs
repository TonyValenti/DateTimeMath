using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateTimeMath.Search {
    public abstract class CompositeCondition : DateTimeCondition, IContainsConditions {

        public List<DateTimeCondition> Conditions { get; private set; } = new List<DateTimeCondition>();

        public CompositeCondition() {

        }

        public override DateTime? NextTime(DateTime CurrentValue) {
            
            var Index = 0;

            var PossibleNextTime = Conditions[Index].NextTime(CurrentValue);

            while(PossibleNextTime != null && !Conditions.TrueForAll(x => x.IsTrue(PossibleNextTime.Value))) {
                Index = (Index + 1) % Conditions.Count;

                PossibleNextTime = Conditions[Index].NextTime(PossibleNextTime.Value);
            }

            return PossibleNextTime;
        }

    }
}
