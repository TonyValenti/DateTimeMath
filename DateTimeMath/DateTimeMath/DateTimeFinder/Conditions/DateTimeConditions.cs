using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateTimeMath.Search {
    public abstract class DateTimeCondition {

        public abstract bool IsTrue(DateTime Value);

        public DateTimeCondition() {
            
        }

    }

}
