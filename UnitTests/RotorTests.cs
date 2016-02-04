using AxialFluxGeneratorDesigner.Calculations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class RotorTests
    {
        private const double Delta = 0.000000000001;

        [TestMethod]
        public void TestPolePairs()
        {
            Assert.AreEqual(8, Rotor.CalculatePolePairs(6));
            Assert.AreEqual(20, Rotor.CalculatePolePairs(15));
            Assert.AreEqual(28, Rotor.CalculatePolePairs(21));
        }


        [TestMethod]
        public void TestMagnetFluxDensity()
        {
            Assert.AreEqual(0.527994086359978, Rotor.CalculateMagnetFluxDensity(1.1, 808, 10, 2), Delta);
            Assert.AreEqual(0.645153110460925, Rotor.CalculateMagnetFluxDensity(1.445, 927.5, 10, 2), Delta);
            Assert.AreEqual(0.645153110460925, Rotor.CalculateMagnetFluxDensity(1.445, 927.5, 15, 2), Delta);
        }

        [TestMethod]
        public void TestMaximumPoleFlux()
        {
            Assert.AreEqual(0.00006451531105, Rotor.CalculateMaximumPoleFlux(0.645153110460925, 10, 10), Delta);
            Assert.AreEqual(0.000096772966575, Rotor.CalculateMaximumPoleFlux(0.645153110460925, 10, 15), Delta);
            Assert.AreEqual(0.000096772966575, Rotor.CalculateMaximumPoleFlux(0.645153110460925, 15, 10), Delta);
        }
    }
}