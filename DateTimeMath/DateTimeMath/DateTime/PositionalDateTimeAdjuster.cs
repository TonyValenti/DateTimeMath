using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateTimeMath {
    public class PositionalDateTimeAdjuster : DateTimeFormula {

        public override IEnumerable<DateTime?> Occurances(DateTime MinDate, DateTime MaxDate, DateTime StartFrom) {

            //AFter = Min, Max, Current
            //Before = Max, Min, Current

            var Query =
                from x in Location.Occurances(MinDate, MaxDate, StartFrom)
                let NewMin = (Position == TimeAdjustmentMode.After ? MinDate : MaxDate)
                let NewMax = (Position == TimeAdjustmentMode.After ? MaxDate : MinDate)
                let Adjusted = Offset.Occurances(NewMin, NewMax, x.Value).FirstOrDefault()
                where Adjusted != null
                select Adjusted
                ;
            
            return Query;
        }

        public DateTimeFormula Offset { get; set; }
        public TimeAdjustmentMode Position { get; set; }
        public DateTimeFormula Location { get; set; }

        public PositionalDateTimeAdjuster(DateTimeFormula Offset, TimeAdjustmentMode Position, DateTimeFormula Location) {
            this.Offset = Offset;
            this.Position = Position;
            this.Location = Location;
        }

        public override string ToString() {
                var ret = "";

                ret = string.Format("{0} {1} {2}", Offset.ToString(), Position, Location.ToString());

                return ret;
        }

    }

    public enum TimeAdjustmentMode {
        After,
        Before,
    }
}
