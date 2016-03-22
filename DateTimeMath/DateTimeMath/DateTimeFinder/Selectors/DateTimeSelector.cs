using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateTimeMath.Search {
    public abstract class DateTimeSelector {
        public abstract IEnumerable<DateTime?> Select(IEnumerable<DateTime?> Source);
    }

}
