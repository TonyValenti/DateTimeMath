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
            if (!IsEmpty) {
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


        public bool IsEmpty {
            get {
                return StartDate >= EndDate;
            }
        }

        public DateTimeRange Intersection(DateTimeRange Item) {
            return Intersection(this, Item);
        }

        public bool Intersects(DateTimeRange Item)
        {
            return Intersects(this, Item);
        }

        public bool Intersects(DateTime DateTime)
        {
            return DateTime >= StartDate && DateTime < EndDate;
        }

        public bool Intersects(DateTime? DateTime) {
            var ret = false;
            if (DateTime.HasValue) {
                ret = Intersects(DateTime.Value);
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
