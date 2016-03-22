using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateTimeMath.Search {

    public class AlternatingPatternSelector : DateTimeSelector {
        public List<bool> Pattern { get; private set; } = new List<bool>();

        public AlternatingPatternSelector() {

        }

        public AlternatingPatternSelector(params bool[] Pattern) {
            this.Pattern.AddRange(Pattern);
        }

        public AlternatingPatternSelector(IEnumerable<bool> Pattern) {
            this.Pattern.AddRange(Pattern);
        }

        public override IEnumerable<DateTime?> Select(IEnumerable<DateTime?> Source) {
            var Index = 0;
            var IE = Source.GetEnumerator();
            while (IE.MoveNext()) {
                var ShouldRet = true;

                if (Pattern.Count > 0) {
                    ShouldRet = Pattern[Index];
                    Index = (Index + 1) % Pattern.Count;
                }

                if (ShouldRet) {
                    yield return IE.Current;
                }
            }
        }
    }



}
