using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateTimeMath.Search {
    public class YearEndsInEvenNumberCondition : DateTimeCondition {

        public override bool IsTrue(DateTime Value) {
            return Value.Year % 2 == 0;
        }

        public override DateTime? NextTime(DateTime CurrentValue) {
            var NextTick = CurrentValue.AddTicks(1);
            
            //Use NextTick.Year because it is slightly possible that adding a tick will cause the year to move.
            var NextYear = new DateTime(NextTick.Year + 1, 1, 1); 
            
            var ret = (IsTrue(NextTick) ? NextTick : NextYear);

            return ret;
        }

    }

    public static partial class DateTimeFinderWithers {
        public static T YearEndsInEvenNumber<T>(this T Composite) where T : IContainsConditions {
            Composite.Conditions<YearEndsInEvenNumberCondition>();
            return Composite;
        }
    }

}
