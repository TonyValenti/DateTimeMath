using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateTimeMath
{
    public class DateTimeRange : IComparable<DateTimeRange>, IEqualityComparer<DateTimeRange>
    {
        public TimeSpan ToTimeSpan() {
            var ret = default(TimeSpan);
            if (HasDuration) {
                ret = EndDate - StartDate;
            }
            return ret;
        }
        
        // >=
        public DateTime StartDate { get; set; }

        // <
        public DateTime EndDate { get; set; }

        public override string ToString()
        {
            return string.Format(
                "{0:yyyy-MM-dd @ HH:mm:ss} TILL {1:yyyy-MM-dd @ HH:mm:ss}",
                StartDate,
                EndDate
                );
        }

        public DateTimeRange Clone()
        {
            return new DateTimeRange(this);
        }

        public DateTimeRange()
        {

        }

        public DateTimeRange(DateTime StartEndDate) : this(StartEndDate, StartEndDate) {
            
        }

        public DateTimeRange(string StartEndDate) : this(StartEndDate, StartEndDate) {

        }

        public DateTimeRange(DateTime StartDate, string EndDate)
        {
            this.StartDate = StartDate;
            this.EndDate = DateTime.Parse(EndDate);
        }

        public DateTimeRange(string StartDate, DateTime EndDate)
        {
            this.StartDate = DateTime.Parse(StartDate);
            this.EndDate = EndDate;
        }

        public DateTimeRange(string StartDate, string EndDate)
        {
            this.StartDate = DateTime.Parse(StartDate);
            this.EndDate = DateTime.Parse(EndDate);
        }

        public DateTimeRange(DateTime StartDate, DateTime EndDate)
        {
            this.StartDate = StartDate;
            this.EndDate = EndDate;
        }

        public DateTimeRange(DateTime StartDate, TimeSpan Offset) {
            this.StartDate = StartDate;
            this.EndDate = StartDate + Offset;
        }

        public DateTimeRange(DateTimeRange Source) : this(Source.StartDate, Source.EndDate)
        {

        }

        public bool HasDuration {
            get {
                return StartDate < EndDate;
            }
        }

        public bool IsMarker {
            get {
                return StartDate == EndDate;
            }
        }

        public bool IsEmpty {
            get {
                return StartDate > EndDate;
            }
        }

        public DateTimeRange Intersection(DateTimeRange Item) {
            return Intersection(this, Item);
        }

        public DateTimeRange Intersection(DateTime StartDate, DateTime EndDate) {
            return Intersection(this, new DateTimeRange(StartDate, EndDate));
        }

        public bool During(DateTimeRange Item)
        {
            return Intersects(this, Item);
        }

        public bool During(DateTime Start, DateTime End) {
            return Intersects(this, new DateTimeRange(Start, End));
        }

        public bool During(DateTime DateTime)
        {
            return Intersects(this, new DateTimeRange(DateTime));
        }

        public bool During(DateTime? DateTime) {
            var ret = false;
            if (DateTime.HasValue) {
                ret = During(DateTime.Value);
            }
            return ret;
        }

        /*
            We have one of the two scenarios below

                /------------\
                |            |
            /---------\
            |         |

                /------------\
                |            |
                        /---------\
                        |         |


            */
        public static DateTimeRange Intersection(DateTimeRange X, DateTimeRange Y) {
            DateTimeRange ret = null;

            if(! X.IsEmpty && !Y.IsEmpty) {

                var BiggestStart = (X.StartDate >= Y.StartDate ? X.StartDate : Y.StartDate);
                var SmallestFinish = (X.EndDate <= Y.EndDate ? X.EndDate : Y.EndDate);

                var NewItem = new DateTimeRange(BiggestStart, SmallestFinish);

                if (!NewItem.IsEmpty) {
                    ret = NewItem;
                }
                
            }

            return ret;
        }

        public static bool Intersects(DateTimeRange X, DateTimeRange Y)
        {
            return Intersection(X, Y) != null;
        }

        public bool Before(DateTimeRange Y) {
            return Before(this, Y);
        }

        public bool Before(DateTime Y) {
            return Before(this, Y);
        }

        public static bool Before(DateTimeRange X, DateTimeRange Y) {
            var ret = false;

            if (!X.IsEmpty && !Y.IsEmpty) {
                ret = X.StartDate < Y.StartDate && X.EndDate <= Y.StartDate;
            }

            return ret;
        }

        public static bool Before(DateTimeRange X, DateTime Y) {
            return Before(X, new DateTimeRange(Y));
        }

        public bool After(DateTimeRange Y) {
            return After(this, Y);
        }

        public bool After(DateTime Y) {
            return After(this, Y);
        }

        public static bool After(DateTimeRange X, DateTimeRange Y) {
            var ret = false;

            if(!X.IsEmpty && !Y.IsEmpty) {
                ret = X.StartDate >= Y.EndDate && X.EndDate > Y.EndDate;
            }

            return ret;
        }

        public static bool After(DateTimeRange X, DateTime Y) {
            return After(X, new DateTimeRange(Y));
        }
        

        public static int Compare(DateTimeRange X, DateTimeRange Y)
        {
            /*
            Logic:
            Empty items have no value and should always come first.
            Two empty items are always equal.
            If two items have the same start date, the one that ends first comes first.
            If two items are not empty, the one that has the earliest start date comes first.
            */

            //If X or Y is empty, make them null.
            X = (Object.ReferenceEquals(X, null) || X.IsEmpty ? null : X);
            Y = (Object.ReferenceEquals(Y, null) || Y.IsEmpty ? null : Y);


            int ret = 0;
            if (Object.ReferenceEquals(X, null) && Object.ReferenceEquals(Y, null)) {
                ret = 0;
            } else if (Object.ReferenceEquals(X, null) && !Object.ReferenceEquals(Y, null))
            {
                ret = -1;
            } else if (!Object.ReferenceEquals(X, null) && Object.ReferenceEquals(Y, null))
            {
                ret = 1;
            } else
            {
                if (X.StartDate < Y.StartDate) {
                    ret = -1;
                } else if (X.StartDate > Y.StartDate)
                {
                    ret = 1;
                } else
                {
                    if (X.EndDate < Y.EndDate)
                    {
                        ret = -1;
                    } else if (X.EndDate > Y.EndDate)
                    {
                        ret = 1;
                    } else
                    {
                        ret = 0;
                    }
                }
            
            }
            

            return ret;
        }

        public int CompareTo(DateTimeRange other)
        {
            return Compare(this, other);
        }

        public static bool operator==(DateTimeRange x, DateTimeRange y)
        {
            return (Compare(x, y) == 0);
        }

        public static bool operator!=(DateTimeRange x, DateTimeRange y)
        {
            return !(Compare(x,y) == 0);
        }

        public static bool operator>(DateTimeRange x, DateTimeRange y)
        {
            return (Compare(x, y) > 0);
        }

        public static bool operator <(DateTimeRange x, DateTimeRange y)
        {
            return (Compare(x, y) < 0);
        }

        public static bool operator <=(DateTimeRange x, DateTimeRange y)
        {
            return (Compare(x, y) <= 0);
        }

        public static bool operator >=(DateTimeRange x, DateTimeRange y)
        {
            return (Compare(x, y) >= 0);
        }


        public static TimeSpan operator+(TimeSpan ts, DateTimeRange dr) {
            return dr.ToTimeSpan() + ts;
        }

        public static TimeSpan operator+(DateTimeRange dr, TimeSpan ts) {
            return dr.ToTimeSpan() + ts;
        }


        public static TimeSpan operator-(TimeSpan ts, DateTimeRange dr) {
            return ts - dr.ToTimeSpan();
        }

        public static TimeSpan operator-(DateTimeRange dr, TimeSpan ts) {
            return dr.ToTimeSpan() - ts;
        }



        public override int GetHashCode()
        {
            var ret = 0;
            if (!this.IsEmpty)
            {
                ret = this.StartDate.GetHashCode() ^ this.EndDate.GetHashCode();
            }

            return ret;
        }

        public override bool Equals(object obj)
        {
            var V = obj as DateTimeRange;

            return this == V;
        }

        public bool Equals(DateTimeRange x, DateTimeRange y)
        {
            return x == y;
        }

        public int GetHashCode(DateTimeRange obj)
        {
            return obj.GetHashCode();
        }
    }
}
