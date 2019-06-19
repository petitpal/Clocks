using Clocks.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ClocksTest
{
    [TestClass]
    public class DecimalClockTests
    {
        [TestMethod]
        public void Midnight()
        {
            DecimalTime testDate = DecimalTime.FromUtc(0, 0, 0);
            Assert.AreEqual(testDate.Hours, 0);
            Assert.AreEqual(testDate.Minutes, 0);
            Assert.AreEqual(testDate.Seconds, 0);
        }

        [TestMethod]
        public void Six()
        {
            DecimalTime testDate = DecimalTime.FromUtc(6, 0, 0);
            Assert.AreEqual(testDate.Hours, 2);
            Assert.AreEqual(testDate.Minutes, 50);
            Assert.AreEqual(testDate.Seconds, 0);
        }

        [TestMethod]
        public void Twleve()
        {
            DecimalTime testDate = DecimalTime.FromUtc(12, 0, 0);
            Assert.AreEqual(testDate.Hours, 5);
            Assert.AreEqual(testDate.Minutes, 0);
            Assert.AreEqual(testDate.Seconds, 0);
        }

        [TestMethod]
        public void Eighteen()
        {
            DecimalTime testDate = DecimalTime.FromUtc(18, 0, 0);
            Assert.AreEqual(testDate.Hours, 7);
            Assert.AreEqual(testDate.Minutes, 50);
            Assert.AreEqual(testDate.Seconds, 0);
        }
    }
}
