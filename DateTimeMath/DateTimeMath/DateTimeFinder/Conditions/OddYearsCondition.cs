using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateTimeMath.Search {
    public class YearEndsInOddNumberCondition : DateTimeCondition {

        public override bool IsTrue(DateTime Value) {
            return Value.Year % 2 == 1;
        }

    }

    public static partial class DateTimeFinderWithers {

        public static T YearEndsInOddNumber<T>(this T Composite) where T : IContainsConditions {
            Composite.Conditions<YearEndsInOddNumberCondition>();
            return Composite;
        }

    }

}
