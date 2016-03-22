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
    }

    public static partial class DateTimeFinderWithers {
        public static T YearEndsInEvenNumber<T>(this T Composite) where T : IContainsConditions {
            Composite.Conditions<YearEndsInEvenNumberCondition>();
            return Composite;
        }
    }

}
