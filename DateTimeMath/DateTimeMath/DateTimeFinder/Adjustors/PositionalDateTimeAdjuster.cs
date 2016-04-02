using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateTimeMath.Search {
    public class PositionalDateTimeAdjuster : DateTimeAdjuster {
        public override IEnumerable<DateTime?> Adjust(DateTimeFormula Offset, DateTime MinDate, DateTime MaxDate, DateTime StartDate) {

            //AFter = Min, Max, Current
            //Before = Max, Min, Current

            var Query =
                from x in Position.Occurances(MinDate, MaxDate, StartDate)
                let NewMin = (Location == TimeAdjustmentLocation.After ? MinDate : MaxDate)
                let NewMax = (Location == TimeAdjustmentLocation.After ? MaxDate : MinDate)
                let Adjusted = Offset.Occurances(NewMin, NewMax, x.Value).FirstOrDefault()
                where Adjusted != null
                select Adjusted
                ;

            return Query;
        }

        public TimeAdjustmentLocation Location { get; set; }
        public DateTimeFormula Position { get; set; }

        
    }

    public enum TimeAdjustmentLocation {
        Before,
        After,
        
    }

    public static partial class IContainsAdjustmentsExtensions {

        public static T Offset<T>(this T DateFinder, TimeAdjustmentLocation Location, DateTimeFormula Position) where T : IContainsAdjustments {
            var Adjuster = new PositionalDateTimeAdjuster();
            Adjuster.Location = Location;
            Adjuster.Position = Position;

            DateFinder.Adjustments.Add(Adjuster);

            return DateFinder;
        }

        public static T Offset<T>(this T DateFinder, TimeAdjustmentLocation Location, Func<T, T> Initializer) where T : DateTimeFormula, IContainsAdjustments, new() {
            return DateFinder.Offset<T, T>(Location, Initializer);
        }

        public static T Offset<T, U>(this T DateFinder, TimeAdjustmentLocation Location, Func<U,U> Initializer) where U : DateTimeFormula, new() where T : IContainsAdjustments {
            var Position = Initializer(new U());

            return DateFinder.Offset(Location, Position);
        }




        public static T Before<T>(this T DateFinder, DateTimeFormula Position) where T : IContainsAdjustments {
            return DateFinder.Offset(TimeAdjustmentLocation.Before, Position);
        }

        public static T Before<T>(this T DateFinder, Func<T,T> Initializer) where T : DateTimeFormula, IContainsAdjustments, new() {
            return DateFinder.Offset<T, T>(TimeAdjustmentLocation.Before, Initializer);
        }

        public static T Before<T, U>(this T DateFinder, Func<U,U> Initializer) where U : DateTimeFormula, new() where T : IContainsAdjustments {
            return DateFinder.Offset<T, U>(TimeAdjustmentLocation.Before, Initializer);
        }






        public static T After<T>(this T DateFinder, DateTimeFormula Position) where T : IContainsAdjustments {
            return DateFinder.Offset(TimeAdjustmentLocation.After, Position);
        }

        public static T After<T>(this T DateFinder, Func<T,T> Initializer) where T : DateTimeFormula, IContainsAdjustments, new() {
            return DateFinder.Offset<T, T>(TimeAdjustmentLocation.After, Initializer);
        }

        public static T After<T, U>(this T DateFinder, Func<U,U> Initializer) where U : DateTimeFormula, new() where T : IContainsAdjustments {
            return DateFinder.Offset<T, U>(TimeAdjustmentLocation.After, Initializer);
        }




    }
}