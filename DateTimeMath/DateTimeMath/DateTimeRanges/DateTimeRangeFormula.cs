using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateTimeMath
{
    

    public class DateTimeRangeFormula 
    {
        /// <summary>
        /// This is normally when the activity begins if no begin date is specified
        /// </summary>
        public DateTimeFormula StartDate { get; set; }

        /// <summary>
        /// This is when the date ends
        /// </summary>
        public DateTimeFormula EndDate { get; set; }

        public DateTimeRangeFormula()
        {

        }

        public DateTimeRangeFormula(DateTimeFormula StartDate, DateTimeFormula EndDate)
        {
            this.StartDate = StartDate;
            this.EndDate = EndDate;
        }

        public IEnumerable<DateSpan> Occurances(DateTime MinStartDate, DateTime MaxEndDate) {
            return Occurances(MinStartDate, MaxEndDate, MinStartDate, MaxEndDate, MinStartDate);
        }

        public IEnumerable<DateSpan> Occurances(DateTime MinStartDate, DateTime MaxStartDate, DateTime MinEndDate, DateTime MaxEndDate, DateTime StartDate) {
            var Query =
                from Start in this.StartDate.Occurances(MinStartDate, MaxStartDate, StartDate)
                let End = (from End in this.EndDate.Occurances(MinEndDate, MaxEndDate, Start.Value) where End > Start select End).FirstOrDefault()
                where Start != null && End != null
                select new DateSpan(Start.Value, End.Value);

            return Query;
        }
  

    }

}
