using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateTimeMath.Search {

    public class FirstSelector : DateTimeSelector {
        public override IEnumerable<DateTime?> Select(IEnumerable<DateTime?> Source) {
            yield return Source.FirstOrDefault();
        }
    }

}
