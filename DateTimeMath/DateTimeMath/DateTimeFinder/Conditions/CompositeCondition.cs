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


    }
}
