using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateTimeMath.Search {
    public abstract class DateTimeModifier {
        public abstract IEnumerable<DateTime?> Modify(IEnumerable<DateTime?> Source);
    }
    



}
