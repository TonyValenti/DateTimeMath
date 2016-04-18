using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using DateTimeMath;
using DateTimeMath.Search;


namespace DateTimeMath.Tests {

    [TestClass]
    public class EnumerableTests {

        [TestMethod]
        public void TestNullable() {
            var Dates = new List<DateTime>();

            var Value1 = (
                from x in Dates
                select x
                ).FirstOrDefault();

            var Value2 = (
                from x in Dates
                select new DateTime?(x)
                ).FirstOrDefault();

            

        }

        [TestMethod]
        public void Performance_Enumerable() {
            var IE = new DateTimeMath.DateTimeEnumerable(DateTime.Now, DateTime.Now.AddYears(5000))
                .On(MonthsOfYear.February)
                .On(DayOfWeek.Sunday)
                .AtMidnight()
                .ToList()
                ;
          
        }

      



    }

}
