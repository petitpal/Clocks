using Clocks.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ClocksTest
{
    [TestClass]
    public class DecimalClockTests
    {
        [DataTestMethod]
        [DataRow(0, 0, 0, 0, 0, 0, 0, 0)]
        [DataRow(6, 0, 0, 0, 2, 50, 0, 0)]
        [DataRow(12, 0, 0, 0, 5, 0, 0, 0)]
        [DataRow(18, 0, 0, 0, 7, 50, 0, 0)]
        [DataRow(0, 1, 0, 0, 0, 0, 69, 444)]
        [DataRow(23, 58, 0, 0, 9, 98, 61, 111)]
        [DataRow(23, 59, 0, 0, 9, 99, 30, 555)]
        public void UtcToDecimal(int utcHours, int utcMinutes, int utcSeconds, int utcMs,
                                 int decHours, int decMinutes, int decSeconds, int decMs)
        {
            // arrange

            // act
            var dt = DecimalTime.FromUtc(utcHours, utcMinutes, utcSeconds, utcMs);
            
            // assert
            Assert.AreEqual(dt.Hours, decHours);
            Assert.AreEqual(dt.Minutes, decMinutes);
            Assert.AreEqual(dt.Seconds, decSeconds);
            Assert.AreEqual(dt.Milliseconds, decMs);
        }




        [TestMethod]
        public void CreateNew()
        {
            // arrange
            var dt = new DecimalTime(1, 2, 3, 4);

            // act

            // assert
            Assert.AreEqual(dt.Hours, 1);
            Assert.AreEqual(dt.Minutes, 2);
            Assert.AreEqual(dt.Seconds, 3);
            Assert.AreEqual(dt.Milliseconds, 4);
        }


        [TestMethod]
        public void Equal()
        {
            // arrange
            var dt1 = new DecimalTime(1, 2, 3, 4);
            var dt2 = new DecimalTime(1, 2, 3, 4);

            // act
            var areEqual = dt1.Equals(dt2);

            // assert
            Assert.IsTrue(areEqual);
        }

        [TestMethod]
        public void NotEqual()
        {
            // arrange
            var dt1 = new DecimalTime(1, 2, 3, 4);
            var dt2 = new DecimalTime(2, 3, 4, 5);

            // act
            var areEqual = dt1.Equals(dt2);

            // assert
            Assert.IsFalse(areEqual);
        }

    }
}
