using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateTimeMath
{
    public abstract class DateTimeFormula
    {
        public IEnumerable<DateTime?> Occurances(DateTime MinDate, DateTime MaxDate) {
            return Occurances(MinDate, MaxDate, MinDate);
        }

        public abstract IEnumerable<DateTime?> Occurances(DateTime MinDate, DateTime MaxDate, DateTime StartDate);

      
    }

}
