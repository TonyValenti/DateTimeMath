using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using DateTimeMath;
using DateTimeMath.Search;
using DateTimeMath.Search.SpecialDays;

namespace DateTimeMath.Tests {
    [TestClass]
    public class DateFinderTests {

        [TestMethod]
        public void Test_Before() {

            var D = new DateTimeFinder()
                .On(DaysOfWeek.Thursday)
                .Before(new DateTimeFinder().On(SpecialDaysEnum.Easter))
                ;

            var Date =
                D.Occurances(new DateTime(2015, 1, 1), new DateTime(2016, 1, 1))
                .FirstOrDefault();
                ;

            Assert.AreEqual(new DateTime(2015, 4, 2), Date);

        }

        [TestMethod]
        public void Test_After() {

            var D = new DateTimeFinder()
                .On(DaysOfWeek.Thursday)
                .After(x => x.On(SpecialDaysEnum.Easter))

            ;
             

            var Date =
                D.Occurances(new DateTime(2015, 1, 1), new DateTime(2016, 1, 1))
                .FirstOrDefault();
            ;

            Assert.AreEqual(new DateTime(2015, 4, 9), Date);

        }

    }
}
