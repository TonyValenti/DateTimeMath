using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateTimeMath.Search {
    public abstract class DateTimeAdjuster {
        public abstract IEnumerable<DateTime?> Adjust(IEnumerable<DateTime?> Source);
    }
    



}
