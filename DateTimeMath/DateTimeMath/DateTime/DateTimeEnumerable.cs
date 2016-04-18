using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DateTimeMath.Search;

namespace DateTimeMath {
    public class DateTimeEnumerable : Search.IContainsConditions, IEnumerable<DateTime> {
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }

        public DateTimeEnumerable(DateTime StartDate, DateTime EndDate) {
            this.StartDate = StartDate;
            this.EndDate = EndDate;
        }

        private List<DateTimeCondition> __Conditions = new List<DateTimeCondition>();
        public List<DateTimeCondition> Conditions {
            get {
                return __Conditions;
            }
        }

        public IEnumerator<DateTime> GetEnumerator() {
            var Condition = new AndCondition();
            Condition.Conditions.AddRange(Conditions);
            if(Conditions.Count == 0) {
                Conditions.Add(new AtTimesCondition(new DateTime()));
            }

            var CurrentDate = new DateTime?(this.StartDate);
            var EndDate = this.EndDate;

            if (Condition.IsTrue(StartDate)) {
                yield return StartDate;
            }

            while (CurrentDate.HasValue && CurrentDate < EndDate) {
                CurrentDate = Condition.NextTime(CurrentDate.Value);
                if(CurrentDate < EndDate) {
                    yield return CurrentDate.Value;
                }
            }

            if (Condition.IsTrue(EndDate)) {
                yield return EndDate;
            }
        }
        
        IEnumerator IEnumerable.GetEnumerator() {
            return this.GetEnumerator();
        }
    }
}
