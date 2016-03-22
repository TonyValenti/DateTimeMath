using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateTimeMath.Search {

    public class OrCondition : CompositeCondition {

        public OrCondition() {

        }

        public OrCondition(IEnumerable<DateTimeCondition> Conditions) {
            this.Conditions.AddRange(Conditions);
        }

        public OrCondition(params DateTimeCondition[] Conditions) {
            this.Conditions.AddRange(Conditions);
        }

        public override bool IsTrue(DateTime Value) {
            var ret = true;
            if (Conditions.Count > 0) {
                ret = Conditions.Any(x => x.IsTrue(Value));
            }
            return ret;
        }
    }
}
