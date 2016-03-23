using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using DateTimeMath;

namespace DateTimeMath.Tests {
    [TestClass]
    public class DateRangeTests {
        [TestMethod]
        public void WarmUp() {

        }

        [TestMethod]
        public void Comparisons_Equal() {
            //Equal Values
            Assert.IsTrue(DateRanges.Day_1_3 == DateRanges.Day_1_3);

            //Null Values
            Assert.IsTrue(DateRanges.Null == DateRanges.Null);

            Assert.IsTrue(DateRanges.Null == DateRanges.EmptyNow);
            Assert.IsTrue(DateRanges.EmptyNow == DateRanges.Null);

            Assert.IsTrue(DateRanges.Null == DateRanges.EmptyReverse_1_3);
            Assert.IsTrue(DateRanges.EmptyReverse_1_3 == DateRanges.Null);

            Assert.IsTrue(DateRanges.Null == DateRanges.EmptyReverse_3_5);
            Assert.IsTrue(DateRanges.EmptyReverse_3_5 == DateRanges.Null);

            //Empty Values
            Assert.IsTrue(DateRanges.EmptyReverse_1_3 == DateRanges.EmptyReverse_3_5);
            Assert.IsTrue(DateRanges.EmptyReverse_3_5 == DateRanges.EmptyReverse_1_3);
        }

        [TestMethod]
        public void Comparisons_NotEqual() {
            //Test the Beginnings
            Assert.IsTrue(DateRanges.Day_1_3 != DateRanges.Day_1_5);
            Assert.IsTrue(DateRanges.Day_1_5 != DateRanges.Day_1_3);

            //Test the Ending Numbers
            Assert.IsTrue(DateRanges.Day_3_5 != DateRanges.Day_1_5);
            Assert.IsTrue(DateRanges.Day_1_5 != DateRanges.Day_3_5);

            //Test Intersections
            Assert.IsTrue(DateRanges.Day_1_3 != DateRanges.Day_2_4);
            Assert.IsTrue(DateRanges.Day_2_4 != DateRanges.Day_1_3);

            //Test Nulls
            Assert.IsTrue(DateRanges.Day_1_3 != DateRanges.Null);
            Assert.IsTrue(DateRanges.Null != DateRanges.Day_1_3);
        }

        [TestMethod]
        public void Comparisons_LessThan() {
            Assert.IsTrue(DateRanges.Day_1_3 < DateRanges.Day_1_5);
            Assert.IsTrue(DateRanges.Day_1_3 < DateRanges.Day_2_4);
            Assert.IsTrue(DateRanges.Null < DateRanges.Day_1_3);
            Assert.IsTrue(DateRanges.EmptyNow < DateRanges.Day_1_3);
            Assert.IsTrue(DateRanges.EmptyReverse_1_3 < DateRanges.Day_1_3);
            Assert.IsTrue(DateRanges.EmptyReverse_3_5 < DateRanges.Day_1_3);
        }

        [TestMethod]
        public void Comparisons_GreaterThan() {
            Assert.IsTrue(DateRanges.Day_1_5 > DateRanges.Day_1_3);
            Assert.IsTrue(DateRanges.Day_2_4 > DateRanges.Day_1_3);
            Assert.IsTrue(DateRanges.Day_1_3 > DateRanges.Null);
            Assert.IsTrue(DateRanges.Day_1_3 > DateRanges.EmptyNow);
            Assert.IsTrue(DateRanges.Day_1_3 > DateRanges.EmptyReverse_1_3);
            Assert.IsTrue(DateRanges.Day_1_3 > DateRanges.EmptyReverse_3_5);
        }


        [TestMethod]
        public void Adjacent_DoesNot_Intersect() {
            var Item0 = DateRanges.Day_1_3;

            var Item1 = DateRanges.Day_3_5;

            Assert.IsFalse(DateTimeRange.Intersects(Item0, Item1));
            Assert.IsFalse(DateTimeRange.Intersects(Item1, Item0));

        }

        [TestMethod]
        public void Intersecting_Does_Intersect_Outside() {
            var Item0 = DateRanges.Day_1_5;

            var Item1 = DateRanges.Day_2_4;

            Assert.IsTrue(DateTimeRange.Intersects(Item0, Item1));
            Assert.IsTrue(DateTimeRange.Intersects(Item1, Item0));

        }

        [TestMethod]
        public void Intersecting_Does_Intersect_Edges() {
            var Item0 = DateRanges.Day_1_3;

            var Item1 = DateRanges.Day_2_4;

            Assert.IsTrue(DateTimeRange.Intersects(Item0, Item1));
            Assert.IsTrue(DateTimeRange.Intersects(Item1, Item0));

        }


    }
}
