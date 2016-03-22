using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateTimeMath.Search {

    
    public class SkipSelector : DateTimeSelector {

        public int ItemsToSkip { get; set; } = 0;

        public SkipSelector() {
            
        }

        public SkipSelector(int ItemsToSkip) {
            this.ItemsToSkip = ItemsToSkip;
        }

        public override IEnumerable<DateTime?> Select(IEnumerable<DateTime?> Source) {
            return Source.Skip(ItemsToSkip);
        }
    }


}
