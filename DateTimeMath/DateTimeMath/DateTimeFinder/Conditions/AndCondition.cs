using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateTimeMath.Search {
    public class AndCondition : CompositeCondition {

        public AndCondition() {

        }

        public AndCondition(IEnumerable<DateTimeCondition> Conditions) {
            this.Conditions.AddRange(Conditions);
        }

        public AndCondition(params DateTimeCondition[] Conditions) {
            this.Conditions.AddRange(Conditions);
        }

        public override bool IsTrue(DateTime Value) {
            var ret = true;
            if (Conditions.Count > 0) {
                ret = Conditions.TrueForAll(x => x.IsTrue(Value));
            }
            return ret;
        }
    }
}
