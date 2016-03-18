using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateTimeMath
{

    public class DateTimeFinder : DateTimeFormula
    {
        public List<TimeCondition> Conditions { get; private set; } = new List<TimeCondition>();
        public List<TimeFilter> Filters { get; private set; } = new List<TimeFilter>();

        public override IEnumerable<DateTime?> Occurances(DateTime MinDate, DateTime MaxDate, DateTime StartDate) {
            var ret = AllDays(MinDate, MaxDate, StartDate);

            ret = (from x in ret where Conditions.TrueForAll(c => c.IsTrue(x.Value)) select x);

            

            foreach (var item in Filters) {
                ret = item.Filter(ret);
            }

            return ret;
        }

        protected IEnumerable<DateTime?> AllDays(DateTime MinDate, DateTime MaxDate, DateTime StartDate) {
            var Modifier = (MinDate < MaxDate ? 1 : -1);
            var LowDate = (MinDate < MaxDate ? MinDate : MaxDate);
            var HighDate = (MaxDate > MinDate ? MaxDate : MinDate);

            var BaseDate = StartDate;

            while (BaseDate >= LowDate && BaseDate <= HighDate) {
                yield return BaseDate;
                BaseDate = BaseDate.AddDays(Modifier);
            }
            

        }


    }
}
